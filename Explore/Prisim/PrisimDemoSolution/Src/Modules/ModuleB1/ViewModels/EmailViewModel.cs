using Infrastructure;
using Prism.Regions;

namespace ModuleB1
{
    public class EmailViewModel : ViewModelBase, IEmailViewModel, INavigationAware
    {
        #region Constructors

        public EmailViewModel()
        {

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
            var toAddress = (string)navigationContext.Parameters["To"];
            if (!string.IsNullOrWhiteSpace(toAddress))
                To = toAddress;
            else
                To = "Email not provided";
        }
    }
}
