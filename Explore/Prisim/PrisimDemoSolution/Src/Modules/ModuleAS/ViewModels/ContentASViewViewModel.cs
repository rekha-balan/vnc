using System.ComponentModel;
using Business;
using Infrastructure.Services;
using System.Collections.ObjectModel;
using Infrastructure;

namespace ModuleAS
{
    public class ContentASViewViewModel : IContentASViewViewModel, INotifyPropertyChanged
    {
        private readonly IPersonService _personService;

        #region Properties

        private ObservableCollection<Person> _People;
        public ObservableCollection<Person> People
        {
            get { return _People; }
            set
            {
                _People = value;
                OnPropertyChanged("People");
            }
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

        public IView View { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        #endregion //Properties

        #region Constructors

        public ContentASViewViewModel(IPersonService personService)
        {
            _personService = personService;
            LoadPeople();
        }

        #endregion //Constructors

        #region Commands



        #endregion //Commands

        #region Methods

        private void LoadPeople()
        {
            IsBusy = true;
            _personService.GetPeopleAsync((sender, result) =>
            {
                People = new ObservableCollection<Person>(result.Object);
                IsBusy = false;
            });
        }

        #endregion //Methods

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion //INotifyPropertyChanged
    }
}
