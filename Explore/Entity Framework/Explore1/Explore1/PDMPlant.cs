//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Explore1
{
    using System;
    using System.Collections.Generic;
    
    public partial class PDMPlant
    {
        public short PlantID { get; set; }
        public string PlantCode { get; set; }
        public string PlantDescription { get; set; }
        public string Location { get; set; }
        public Nullable<int> SafetyRating { get; set; }
        public Nullable<int> ErgoRating { get; set; }
        public Nullable<int> TPMRating { get; set; }
        public Nullable<short> Net { get; set; }
        public string CheckEngineer { get; set; }
    }
}