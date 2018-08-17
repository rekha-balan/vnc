using System;
using System.ComponentModel;

namespace Infrastructure
{
    public class ViewModelBase : IViewModel, IViewModel2, INotifyPropertyChanged
    {
        public IView View { get; set; }

        public ViewModelBase() { }

        public ViewModelBase(IView view)
        {
            View = view;
            View.ViewModel = this;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
