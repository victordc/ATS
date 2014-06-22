using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATS.MVC.UI.Webforms
{
    public class LeaveReport
    {
        public int LeavePlanId { get; set; }
        public string LeaveType { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Duration { get; set; }
        public Nullable<bool> Admitted { get; set; }
        public Nullable<double> Credit { get; set; }
    }
}