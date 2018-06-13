using System;
using PluralsightPrismDemo.Infrastructure;
using Microsoft.Practices.Prism.Events;

namespace PluralsightPrismDemo.StatusBar
{
    public class StatusBarViewModel : ViewModelBase, IStatusBarViewModel
    {
        IEventAggregator _eventAggregator;

        public StatusBarViewModel(IStatusBarView view, IEventAggregator eventAggregator)
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
        private void PersonUpdated(string obj)
        {
            Message = String.Format("{0} was updated.", obj);
        }
    }
}
