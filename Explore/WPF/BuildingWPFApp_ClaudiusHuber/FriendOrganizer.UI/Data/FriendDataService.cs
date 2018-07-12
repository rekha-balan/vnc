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
        // Looks like the same syntax as AutoFac works.  Create a Func<> that
        // knows how to return a FriendOrganizerDbContext
        // See the Bootstraper class for how it is registered.

        public FriendDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public IEnumerable<Friend> GetAll()
        {
            //// TODO(crhodes)
            //// Load data from real database.
            //// For now just return hard coded list.
            //yield return new Friend { FirstName = "Vikki", LastName = "Schanz" };
            //yield return new Friend { FirstName = "Natalie", LastName = "Rhodes" };
            //yield return new Friend { FirstName = "Christopher", LastName = "Rhodes" };
            //yield return new Friend { FirstName = "George", LastName = "Lachow" };

            // Shouldn't have a dependency here.  Should have dependency injected.

            //using (var ctx = new FriendOrganizerDbContext())
            //{
            //    return ctx.Friends.AsNoTracking().ToList();
            //}


            using (var ctx = _contextCreator())
            {
                return ctx.Friends.AsNoTracking().ToList();
            }
        }
    }
}
