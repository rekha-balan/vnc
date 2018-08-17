using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NinjaDomain.Classes
{
    public class NinjaEquipment
    {
        #region Enums, Fields, Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public EquipmentType Type { get; set; }

        // Adding the [Required] data annotation 
        // forces the relationship to be 1 to many instead of 0 to many.
        // Since there is no FK back to Ninja, EF will assume Nija Not Required
        // for Ninja Equipment
        // NB.  This "pollutes" domain model with EF stuff, however,
        // Data Annotations can be useful.
        // Alternatively can use fluent API directly on DbContext

        [Required]
        public Ninja Ninja { get; set; }

        //public DateTime DateCreated { get; set; }

        //public DateTime DateModified { get; set; }

        public Nullable<DateTime> DateCreated { get; set; }

        public Nullable<DateTime> DateModified { get; set; }

        public bool IsDirty { get; set; }

        #endregion
    }
}
