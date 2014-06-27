using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Data.DAL;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web;
using ATS.Data.Model;

namespace ATS.Data.ViewModel
{
    public class TimeSheetMasterViewModel
    {
        public int TimeSheetMasterId { get; set; }
        public int PersonId { get; set; }
        public Nullable<int> ManagerId { get; set; }
        public Nullable<int> AgencyId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public virtual IList<TimeSheetDetailViewModel> TimeSheetDetailViewModel { get; set; }

        [DisplayName("Staff Name")]
        public string PersonPersonName { get; set; }
        public string PersonUserName { get; set; }
        [DisplayName("Manager")]
        public string SupervisorPersonName { get; set; }
        [DisplayName("Company")]
        public string CompanyCompanyDescription { get; set; }
        [DisplayName("Agent Name")]
        public string AgentPersonName { get; set; }
    }

    public class TimeSheetDetailViewModel
    {
        public int TimeSheetDetailId { get; set; }
        public int TimeSheetMasterId { get; set; }
        public int CalendarYearId { get; set; }
        public Nullable<int> LeaveCategoryId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TimeSheetDate { get; set; }
        //[Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        public DateTime EndTime { get; set; }
        public float Hour { get; set; }
        public string Remarks { get; set; }
        public string SupportDocument1 { get; set; }
        public string SupportDocument2 { get; set; }
        public string SupportDocument3 { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase SupportDocumentUpload1 { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase SupportDocumentUpload2 { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase SupportDocumentUpload3 { get; set; }

        public SelectList LeaveCategories { get; set; }
        public virtual TimeSheetMaster TimeSheetMaster { get; set; }
    }
}
