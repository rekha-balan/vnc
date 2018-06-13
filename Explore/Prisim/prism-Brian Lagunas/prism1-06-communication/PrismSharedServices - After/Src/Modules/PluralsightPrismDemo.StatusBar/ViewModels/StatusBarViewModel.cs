using System;
using PluralsightPrismDemo.Infrastructure;

namespace PluralsightPrismDemo.StatusBar
{
    public class StatusBarViewModel : ViewModelBase, IStatusBarViewModel
    {
        public StatusBarViewModel(IStatusBarView view)
            : base(view)
        {
        }

        private string _message = "Ready";
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }
    }
}
