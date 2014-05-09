using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    [MetadataType(typeof(StaffMetadata))]
    public partial class Staff
    {
        //public static IEnumerable<Staff> GetAllStaffs()
        //{
        //    ATSCEEntities context = new ATSCEEntities();
        //    return context.Persons.OfType<Staff>().ToList();
        //}

        //public static Staff GetStaffById(int personId)
        //{
        //    ATSCEEntities context = new ATSCEEntities();
        //    IQueryable<Staff> query = from s in context.Persons.OfType<Staff>()
        //                where s.PersonId == personId 
        //                select s;
        //    Staff staff = query.FirstOrDefault<Staff>();
        //    return staff;
        //}

    }

    public class StaffMetadata
    {
        //[Required(ErrorMessage = "Agent is required")]
        [DisplayName("Agent")]
        public int AgentId { get; set; }

        //[Required(ErrorMessage = "Supervisor is required")]
        [DisplayName("Supervisor")]
        public int SupervisorId { get; set; }
    }
}
