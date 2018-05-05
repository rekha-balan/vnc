using System;
using PrismDemo.Infrastructure;
using PrismDemo.Business;
//using Microsoft.Practices.Prism.Commands;
using Prism.Commands;

namespace PrismDemo.People
{
    public class PersonViewModel : ViewModelBase, IPersonViewModel
    {
        public DelegateCommand<Person> SaveCommand { get; set; }

        public PersonViewModel(IPersonView view)
            : base(view)
        {
            CreatePerson();

            SaveCommand = new DelegateCommand<Person>(Save, CanSave);
        }

        private void Save(Person value)
        {
            Person.LastUpdated = DateTime.Now.AddYears(value.Age);
        }
        private bool CanSave(Person value)
        {
            return Person.Error == null;
        }

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

        void Person_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void CreatePerson()
        {
            Person = new Person()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Age = 46
            };
        }
    }
}
