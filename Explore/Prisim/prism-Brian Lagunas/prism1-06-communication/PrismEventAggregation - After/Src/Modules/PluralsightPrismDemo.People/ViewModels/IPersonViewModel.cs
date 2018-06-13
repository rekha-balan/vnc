using System;
using PluralsightPrismDemo.Infrastructure;

namespace PluralsightPrismDemo.People
{
    public interface IPersonViewModel : IViewModel
    {
        void CreatePerson(string firstName, string lastName);
    }
}
