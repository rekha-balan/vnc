using System;
using PluralsightPrismDemo.Infrastructure;
using PluralsightPrismDemo.Business;
using System.Collections.ObjectModel;

namespace PluralsightPrismDemo.People
{
    public class PeopleViewModel : ViewModelBase, IPeopleViewModel
    {
        public PeopleViewModel(IPeopleView view)
            : base(view)
        {
            CreatePeople();
        }

        private ObservableCollection<Person> _people;
        public ObservableCollection<Person> People
        {
            get { return _people; }
            set
            {
                _people = value;
                OnPropertyChanged("People");
            }
        }

        private void CreatePeople()
        {
            var people = new ObservableCollection<Person>();
            for (int i = 0; i < 10; i++)
            {
                people.Add(new Person()
                {
                    FirstName = String.Format("First {0}", i),
                    LastName = String.Format("Last {0}", i)
                });
            }

            People = people;
        }
    }
}
