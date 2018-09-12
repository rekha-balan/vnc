using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Infrastructure
{
    public class ViewModelBase : IViewModel_VM1, IViewModel_V1, INotifyPropertyChanged
    {
        public IView_VM1 View { get; set; }

        public ViewModelBase() { }

        public ViewModelBase(IView_VM1 view)
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

        // This is the traditional approach - requires string name to be passed in

        //private void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        // This is the new CompilerServices attribute!

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
