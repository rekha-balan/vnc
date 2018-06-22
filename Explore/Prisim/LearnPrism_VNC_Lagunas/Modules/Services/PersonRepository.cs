using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;

namespace Services
{
    public class PersonRepository : IPersonRepository
    {
        // Use this to show we are getting the same instance of service.
        int count = 0;

        public int SavePerson(Business.Person person)
        {
            count++;
            person.LastUpdated = DateTime.Now;
            return count;
        }
    }
}
