using System;
using Infrastructure;
//using Business;
//using Microsoft.Practices.Prism.Commands;
using Prism.Commands;
using Prism.Events;
using System.Windows;
using ModuleInterfaces;

namespace PeopleCC
{
    public class PersonViewModel : ViewModelBase, IPersonViewModel
    {
        #region "Constructors, Initialization, and Load"

        public PersonViewModel(IPerson view)
            : base(view)
        {
            SaveCommand = new DelegateCommand(Save, CanSave);

            GlobalCommands.SaveAllCommand.RegisterCommand(SaveCommand);
        }

        #endregion

        #region Enums, Fields, Properties

        private Business.Person _person;
        public Business.Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                // Hook in event handler to force (re)check of CanSave
                _person.PropertyChanged += Person_PropertyChanged;
                OnPropertyChanged("Person");
            }
        }

        public string ViewName
        {
            get
            {
                return string.Format("{0}, {1}", Person.LastName, Person.FirstName);
            }
        }

        #endregion

        #region Public Methods

        public void CreatePerson(string firstName, string lastName)
        {
            Person = new Business.Person()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = 0 // This is an invalid age.  Must correct before saving.
            };
        }

        #endregion

        #region Private Methods

        private void Person_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Force calling of CanSave on SaveCommand delegate
            SaveCommand.RaiseCanExecuteChanged();
        }

        #region DelegateCommand taking no parameters

        public DelegateCommand SaveCommand { get; set; }

        private bool CanSave()
        {
            return Person != null && Person.Error == null;
        }

        private void Save()
        {
            Person.LastUpdated = DateTime.Now;
        }

        #endregion

        #endregion
    }
}
