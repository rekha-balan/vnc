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
    
    public partial class PDMRemedyPlan
    {
        public decimal DocID { get; set; }
        public decimal DocSeq { get; set; }
        public string DocRecType { get; set; }
        public decimal Seq { get; set; }
        public string FailureDesc { get; set; }
        public string OrigOpno { get; set; }
        public string Severity { get; set; }
        public string ReworkDesc { get; set; }
        public Nullable<decimal> LowerValue { get; set; }
        public Nullable<decimal> UpperValue { get; set; }
        public string UnitNo { get; set; }
        public string ReworkWho { get; set; }
        public string ReworkWhere { get; set; }
        public string StopBuild { get; set; }
        public string NotifyStage { get; set; }
        public string NotifySupervisor { get; set; }
        public string NotifyQC { get; set; }
        public string NotifyME { get; set; }
        public string NotifyEng { get; set; }
        public string NotifyMat { get; set; }
        public string RaiseCAR { get; set; }
        public string Comments { get; set; }
        public string InputRequired { get; set; }
        public string CheckMethod { get; set; }
        public Nullable<decimal> PlanType { get; set; }
        public string OPERATORID { get; set; }
    }
}
