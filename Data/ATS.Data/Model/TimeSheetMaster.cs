//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATS.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TimeSheetMaster
    {
        public int TimeSheetMasterId { get; set; }
        public int PersonId { get; set; }
        public Nullable<int> ManagerId { get; set; }
        public Nullable<int> AgencyId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual Company Company { get; set; }
        public virtual Supervisor Supervisor { get; set; }
    }
}
