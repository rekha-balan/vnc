using Microsoft.Practices.Prism.Regions;
using PluralsightPrismDemo.Infrastructure;

namespace ModuleA
{
    public class EmailViewViewModel : ViewModelBase, IEmailViewViewModel, INavigationAware
    {
        #region Constructors

        public EmailViewViewModel()
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
            var toAddress = navigationContext.Parameters["To"];
            if (!string.IsNullOrWhiteSpace(toAddress))
                To = toAddress;
            else
                To = "Email not provided";
        }
    }
}
