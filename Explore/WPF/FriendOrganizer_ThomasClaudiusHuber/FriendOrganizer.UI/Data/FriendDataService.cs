using System;
using System.Collections.Generic;
using System.Linq;
using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        private FriendOrganizerDbContext _dbContext;
        Func<FriendOrganizerDbContext> _contextCreator;

        //public FriendDataService(FriendOrganizerDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        // TODO(crhodes)
        // Need to figure out how to get Unity to pass this in.

        public FriendDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public IEnumerable<Friend> GetAll()
        {
            //// TODO(crhodes)
            //// Load data from real database.
            //yield return new Friend { FirstName = "Vikki", LastName = "Schanz" };
            //yield return new Friend { FirstName = "Natalie", LastName = "Rhodes" };
            //yield return new Friend { FirstName = "Christopher", LastName = "Rhodes" };
            //yield return new Friend { FirstName = "George", LastName = "Lachow" };

            // Shouldn't have a dependency here.  Should pass in.

            //using (var ctx = new FriendOrganizerDbContext())
            using (var ctx = _contextCreator())
            {
                return ctx.Friends.AsNoTracking().ToList();
            }
        }
    }
}
