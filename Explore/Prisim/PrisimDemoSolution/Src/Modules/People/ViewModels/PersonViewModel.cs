using System;
using Infrastructure;
using Business;
//using Microsoft.Practices.Prism.Commands;
using Prism.Commands;
using Prism.Events;
using System.Windows;

namespace People
{
    public class PersonViewModel : ViewModelBase, IPersonViewModel
    {
        #region "Constructors, Initialization, and Load"

        public PersonViewModel(IPersonView view)
            : base(view)
        {
            CreatePerson();

            // Use this form if do not need/want to pass parameters to methods
            //SaveCommand = new DelegateCommand(Save, CanSave);

            // Use this form to pass nullable command parameter.  Use object or other nullable type.
            SaveCommand = new DelegateCommand<Person>(Save, CanSave);
        }

        //public PersonViewModel(IPersonView view, IEventAggregator eventAggregator, IPersonRepository personRepository)
        //    : base(view)
        //{
        //    _eventAggregator = eventAggregator;
        //    _personRepository = personRepository;

        //    SaveCommand = new DelegateCommand(Save, CanSave);

        //    GlobalCommands.SaveAllCommand.RegisterCommand(SaveCommand);
        //}

        #endregion

        #region Enums, Fields, Properties

        IEventAggregator _eventAggregator;
        IPersonRepository _personRepository;

        private Person _person;
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                _person.PropertyChanged += Person_PropertyChanged;
                OnPropertyChanged("Person");
            }
        }

        //public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand<Person> SaveCommand { get; set; }

        public string ViewName
        {
            get
            {
                return string.Format("{0}, {1}", Person.LastName, Person.FirstName);
            }
        }

        #endregion

        #region Public Methods

        private void CreatePerson()
        {
            Person = new Person()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Age = 46
            };
        }

        public void CreatePerson(string firstName, string lastName)
        {
            Person = new Person()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = 0 // This is an invalid age.  Must correct before saving.
            };
        }

        #endregion

        #region Private Methods

        private bool CanSave()
        {
            return Person != null && Person.Error == null;
        }

        private void Person_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void Save()
        {
            Person.LastUpdated = DateTime.Now;
            //_eventAggregator.GetEvent<PersonUpdatedEvent>().Publish(string.Format("{0}, {1}", Person.LastName, Person.FirstName));
            //int count = _personRepository.SavePerson(Person);
            //MessageBox.Show(count.ToString());
        }

        private void Save(Person value)
        {
            Person.LastUpdated = DateTime.Now.AddYears(value.Age);
        }

        private bool CanSave(Person value)
        {

            return Person.Error == null;
        }

        #endregion

    }
}
