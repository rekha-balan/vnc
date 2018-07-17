using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        //If use await then must mark method with async
       public async Task<List<Friend>> GetAllAsync()
        {
            using (var ctx = _contextCreator())
            {
                // Await result so ctx doesn't get disposed before ToListAsync returns

                return await ctx.Friends.AsNoTracking().ToListAsync();

                //// Demonstrate UI remains responsive

                //var friends = await ctx.Friends.AsNoTracking().ToListAsync();

                //// See that can move window around
                //await Task.Delay(5000);

                //// And then friends show up.
                //return friends;
            }
        }

        public async Task<Friend> GetByIdAsync(int friendId)
        {
            using (var ctx = _contextCreator())
            {
                // Await result so ctx doesn't get disposed before ToListAsync returns

                return await ctx.Friends.AsNoTracking().SingleAsync(f => f.Id == friendId);

                //// Demonstrate UI remains responsive

                //var friends = await ctx.Friends.AsNoTracking().ToListAsync();

                //// See that can move window around
                //await Task.Delay(5000);

                //// And then friends show up.
                //return friends;
            }
        }
    }
}
