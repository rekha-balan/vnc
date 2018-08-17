using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NinjaDomain.Classes
{
    // These are all in same file so easier to see.

    public class Ninja
    {

        #region Enums, Fields, Properties

        public Clan Clan { get; set; }

        public int ClanId { get; set; }

        public System.DateTime DateOfBirth { get; set; }

        public System.DateTime? DateOfDeath { get; set; }

        public List<NinjaEquipment> EquipmentOwned { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool ServedInOniwaban { get; set; }

        #endregion

    }

    public class Clan
    {

        #region Enums, Fields, Properties

        public string ClanName { get; set; }

        public int Id { get; set; }

        public List<Ninja> Ninjas { get; set; }

        #endregion

    }

    public class NinjaEquipment
    {

        #region Enums, Fields, Properties

        public int Id { get; set; }

        public string Name { get; set; }

        // Adding the [Required] data annotation 
        // forces the relationship to be 1 to many instead of 0 to many.
        // Since there is no FK back to Ninja, EF will assume Nija Not Required
        // for Ninja Equipment
        // NB.  This "pollutes" domain model with EF stuff, however,
        // Data Annotations can be useful.
        // Alternatively can use fluent API directly on DbContext

        [Required]
        public Ninja Ninja { get; set; }

        public EquipmentType Type { get; set; }

        #endregion

    }
}
