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
    
    public partial class Person
    {
        public Person()
        {
            this.LeavePlans = new HashSet<LeavePlan>();
        }
    
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string Phone { get; set; }
        public string HomeAddress { get; set; }
        public string UserName { get; set; }
    
        public virtual ICollection<LeavePlan> LeavePlans { get; set; }
    }
}
