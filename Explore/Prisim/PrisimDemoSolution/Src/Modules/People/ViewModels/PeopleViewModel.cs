using System;
using Infrastructure;
using System.Collections.ObjectModel;

namespace People
{
    public class PeopleViewModel : ViewModelBase, IPeopleViewModel
    {
        public PeopleViewModel(IPeople view)
            : base(view)
        {
            CreatePeople();
        }

        private ObservableCollection<Business.Person> _people;
        public ObservableCollection<Business.Person> People
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
            var people = new ObservableCollection<Business.Person>();
            for (int i = 0; i < 5; i++)
            {
                people.Add(new Business.Person()
                {
                    FirstName = String.Format("First {0}", i),
                    LastName = String.Format("Last {0}", i)
                });
            }

            People = people;
        }
    }
}
