using System;
using Infrastructure;
using Business;

namespace ModuleInterfaces
{
    public interface IPersonDetailsViewModel : IViewModel_VM1
    {
        Person SelectedPerson { get; set; }
    }
}
