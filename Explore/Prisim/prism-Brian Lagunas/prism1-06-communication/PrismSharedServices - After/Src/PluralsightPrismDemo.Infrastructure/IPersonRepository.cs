using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluralsightPrismDemo.Business;

namespace PluralsightPrismDemo.Infrastructure
{
    public interface IPersonRepository
    {
        int SavePerson(Person person);
    }
}
