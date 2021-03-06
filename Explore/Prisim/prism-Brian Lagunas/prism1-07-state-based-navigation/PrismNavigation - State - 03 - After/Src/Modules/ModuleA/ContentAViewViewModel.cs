﻿using System.Collections.Generic;
using System.ComponentModel;
using Business;
using Microsoft.Practices.Prism.Commands;
using PluralsightPrismDemo.Infrastructure.Services;
using System.Collections.ObjectModel;
using Microsoft.Windows.Controls;

namespace ModuleA
{
    public class ContentAViewViewModel : IContentAViewViewModel, INotifyPropertyChanged
    {
        private readonly IPersonService _personService;

        #region Properties

        public DelegateCommand EditPersonCommand { get; private set; }

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

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                EditPersonCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("SelectedPerson");
            }
        }

        private WindowState _windowState;
        public WindowState WindowState
        {
            get { return _windowState; }
            set
            {
                _windowState = value;
                OnPropertyChanged("WindowState");
            }
        }

        #endregion //Properties

        #region Constructors

        public ContentAViewViewModel(IPersonService personService)
        {
            _personService = personService;
            LoadPeople();
            EditPersonCommand = new DelegateCommand(EditPerson, CanEditPerson);
        }

        #endregion //Constructors

        #region Commands

        private void EditPerson()
        {
            WindowState = Microsoft.Windows.Controls.WindowState.Open;
        }

        private bool CanEditPerson()
        {
            return SelectedPerson != null;
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
