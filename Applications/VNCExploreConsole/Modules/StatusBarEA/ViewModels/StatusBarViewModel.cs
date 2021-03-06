using System;
using Prism.Events;
using Infrastructure;
using ModuleInterfaces;

namespace StatusBarEA
{
    public class StatusBarViewModel : ViewModelBase, IStatusBarViewModel
    {
        IEventAggregator _eventAggregator;

        public StatusBarViewModel(IStatusBar view, IEventAggregator eventAggregator)
            : base(view)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PersonUpdatedEvent>().Subscribe(PersonUpdated);
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

        void PersonUpdated(string obj)
        {
            Message = string.Format("{0} was updated", obj);
        }
    }
}

