using System;
using Infrastructure;
using Business;
using ModuleInterfaces;

namespace PeopleDC
{
    public class PersonDetailsViewModel : ViewModelBase, IPersonDetailsViewModel
    {
        public PersonDetailsViewModel() { }

        private Business.Person _SelectedPerson;
        public Business.Person SelectedPerson
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
