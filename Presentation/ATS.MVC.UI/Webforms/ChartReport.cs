using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATS.MVC.UI.Webforms
{
    public class ChartReport
    {
        public string StartDate { get; set; }
        public double MC { get; set; }
        public double Annual { get; set; }
        public double Exams { get; set; }
    }
}