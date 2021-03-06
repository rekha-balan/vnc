using Infrastructure;
using Prism.Commands;
using Prism.Regions;

namespace ModuleB1
{
    public class EmailViewModel : ViewModelBase, IEmailViewModel, INavigationAware
    {
        private IRegionNavigationJournal _navigationJournal;

        #region Constructors

        public EmailViewModel()
        {
            CancelCommand = new DelegateCommand(Cancel);
        }

        #endregion //Constructors

        #region Properties

        private string _to;
        public string To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                OnPropertyChanged("Subject");
            }
        }

        private string _body;
        public string Body
        {
            get { return _body; }
            set
            {
                _body = value;
                OnPropertyChanged("Body");
            }
        }

        public DelegateCommand CancelCommand { get; private set; }

        #endregion //Properties

        #region Commands

        private void Cancel()
        {
            _navigationJournal.GoBack();
        }


        #endregion //Commands
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //return true;
            var toAddress = navigationContext.Parameters["To"];

            // Ensure we a get a unique view for each target.

            if (To == (string)toAddress)
                return true;
            else
                return false;   // force creation of new one
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationJournal = navigationContext.NavigationService.Journal;

            var toAddress = (string)navigationContext.Parameters["To"];
            if (!string.IsNullOrWhiteSpace(toAddress))
                To = toAddress;
            else
                To = "Email not provided";
        }
    }
}
