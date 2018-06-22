using System;
using Infrastructure;
using Business;

namespace People
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
