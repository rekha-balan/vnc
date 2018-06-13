using System;
using PluralsightPrismDemo.Infrastructure;
using PluralsightPrismDemo.Business;
using Microsoft.Practices.Prism.Commands;
using System.Windows;

namespace PluralsightPrismDemo.People
{
    public class PersonViewModel : ViewModelBase, IPersonViewModel
    {
        public string ViewName
        {
            get
            {
                return string.Format("{0}, {1}", Person.LastName, Person.FirstName);
            }
        }

        public DelegateCommand SaveCommand { get; set; }

        IPersonRepository _repository;

        public PersonViewModel(IPersonView view, IPersonRepository repository)
            : base(view)
        {
            _repository = repository;
            SaveCommand = new DelegateCommand(Save, CanSave);
        }

        private void Save()
        {
            int count = _repository.SavePerson(Person);
            MessageBox.Show(count.ToString());
        }
        private bool CanSave()
        {
            return Person != null && Person.Error == null;
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

        public void CreatePerson(string firstName, string lastName)
        {
            Person = new Person()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = 0
            };
        }
    }
}
