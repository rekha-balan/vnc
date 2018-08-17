using System;
using System.Collections.Generic;

namespace NinjaDomain.Classes
{
    // These are all in same file so easier to see.

    public class Ninja
    {
        public Ninja()
        {
            EquipmentOwned = new List<NinjaEquipment>();
        }

        #region Enums, Fields, Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public bool ServedInOniwaban { get; set; }

        public Clan Clan { get; set; }

        public int ClanId { get; set; }

        public List<NinjaEquipment> EquipmentOwned { get; set; }

        public DateTime DateOfBirth { get; set; }

        public System.DateTime? DateOfDeath { get; set; }

        //public DateTime DateCreated { get; set; }

        //public DateTime DateModified { get; set; }

        public Nullable<DateTime> DateCreated { get; set; }

        public Nullable<DateTime> DateModified { get; set; }

        public bool IsDirty { get; set; }

        #endregion

    }
}
