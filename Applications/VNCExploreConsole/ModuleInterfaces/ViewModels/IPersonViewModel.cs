using System;
using Infrastructure;

namespace ModuleInterfaces
{
    public interface IPersonViewModel : IViewModel_VM1
    {
        void CreatePerson(string firstName, string lastName);
    }
}
