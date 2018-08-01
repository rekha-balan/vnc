using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class User
    {
        #region Properties

        /// <summary>
        /// Property to get/set ProfileId
        /// </summary>
        public string ProfileId
        {
            get;
            set;
        }

        /// <summary>
        /// Property to get/set multiple ConnectionId
        /// </summary>
        public HashSet<string> ConnectionIds
        {
            get;
            set;
        }

        #endregion
    }
}
