using System;
using Prism.Events;
using Infrastructure;
using ModuleInterfaces;

namespace StatusBar
{
    public class StatusBarViewModel : ViewModelBase, IStatusBarViewModel
    {
        public StatusBarViewModel(IStatusBar view)
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

