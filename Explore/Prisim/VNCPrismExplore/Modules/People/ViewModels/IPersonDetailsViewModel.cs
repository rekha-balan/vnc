using System;
using Infrastructure;
using Business;

namespace People
{
    public interface IPersonDetailsViewModel : IViewModel
    {
        Person SelectedPerson { get; set; }
    }
}
