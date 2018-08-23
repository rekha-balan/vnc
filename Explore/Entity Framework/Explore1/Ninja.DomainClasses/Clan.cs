using NinjaDomain.Classes.Interfaces;
using System;
using System.Collections.Generic;

namespace NinjaDomain.Classes
{
    public class Clan : IModificationHistory
    {
        public Clan()
        {
            Ninjas = new List<Ninja>();
        }

        #region Enums, Fields, Properties

        public int Id { get; set; }

        public string ClanName { get; set; }

        public List<Ninja> Ninjas { get; set; }

        public Nullable<DateTime> DateCreated { get; set; }
        
        public Nullable<DateTime> DateModified { get; set; }

        public bool IsDirty { get; set; }

        #endregion
    }
}
