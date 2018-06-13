using System;
using System.Collections.ObjectModel;
using Business;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using PluralsightPrismDemo.Infrastructure;
using PluralsightPrismDemo.Infrastructure.Services;

namespace ModuleA
{
    public class ViewAViewModel : ViewModelBase, IViewAViewModel
    {
        private readonly IRegionManager _regionManager;
        private readonly IPersonService _personService;

        #region Constructors

        public ViewAViewModel(IRegionManager regionManager, IPersonService personService)
        {
            _regionManager = regionManager;
            _personService = personService;
            EmailCommand = new DelegateCommand<Person>(Email);
            LoadPeople();
        }

        #endregion //Constructors

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

        public DelegateCommand<Person> EmailCommand { get; private set; }

        #endregion //Properties

        #region Commands

        private void Email(Person person)
        {
            if (person != null)
            {
                var uriQuery = new UriQuery();
                uriQuery.Add("To", person.Email);

                var uri = new Uri(typeof(EmailView).FullName + uriQuery, UriKind.Relative);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, uri);
            }
        }

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
    }
}
