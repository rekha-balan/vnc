using System;
using PrismDemo.Infrastructure;

namespace PrismDemo.StatusBar
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
