using System;
using PluralsightPrismDemo.Infrastructure;
using PluralsightPrismDemo.Business;

namespace PluralsightPrismDemo.People
{
    public class PersonDetailsViewModel : ViewModelBase, IPersonDetailsViewModel
    {
        public PersonDetailsViewModel() { }

        private Person _SelectedPerson;
        public Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {
                _SelectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }
    }
}
