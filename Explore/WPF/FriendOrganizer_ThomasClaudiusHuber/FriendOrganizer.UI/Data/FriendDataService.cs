using System.Collections.Generic;
using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {

        public IEnumerable<Friend> GetAll()
        {
            // TODO(crhodes)
            // Load data from real database.
            yield return new Friend { FirstName = "Vikki", LastName = "Schanz" };
            yield return new Friend { FirstName = "Natalie", LastName = "Rhodes" };
            yield return new Friend { FirstName = "Christopher", LastName = "Rhodes" };
            yield return new Friend { FirstName = "George", LastName = "Lachow" };
        }
    }
}
