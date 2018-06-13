using Microsoft.Practices.Prism.Regions;
using PluralsightPrismDemo.Infrastructure;
using Microsoft.Practices.Prism.Commands;

namespace ModuleA
{
    public class EmailViewViewModel : ViewModelBase, IEmailViewViewModel, INavigationAware
    {
        private IRegionNavigationJournal _navigationJournal;

        #region Constructors

        public EmailViewViewModel()
        {
            CancelCommand = new DelegateCommand(Cancel);
        }

        #endregion //Constructors

        #region Commands

        private void Cancel()
        {
            _navigationJournal.GoBack();
        }

        #endregion //Commands

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

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationJournal = navigationContext.NavigationService.Journal;

            var toAddress = navigationContext.Parameters["To"];
            if (!string.IsNullOrWhiteSpace(toAddress))
                To = toAddress;
            else
                To = "Email not provided";
        }
    }
}
