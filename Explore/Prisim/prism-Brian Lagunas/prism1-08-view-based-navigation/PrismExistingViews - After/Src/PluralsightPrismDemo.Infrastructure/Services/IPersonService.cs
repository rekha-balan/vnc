using System.Collections.Generic;
using Business;
using System;

namespace PluralsightPrismDemo.Infrastructure.Services
{
    public interface IPersonService
    {
        IList<Person> GetPeople();
        void GetPeopleAsync(EventHandler<ServiceResult<IList<Person>>> callback);
    }
}