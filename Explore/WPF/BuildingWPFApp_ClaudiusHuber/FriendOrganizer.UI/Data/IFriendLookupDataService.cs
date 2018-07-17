using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data
{
    public interface IFriendLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetFriendLookupAsync();
    }
}
