using System;
using PluralsightPrismDemo.Infrastructure;
using PluralsightPrismDemo.Business;

namespace PluralsightPrismDemo.People
{
    public interface IPersonDetailsViewModel : IViewModel
    {
        Person SelectedPerson { get; set; }
    }
}
