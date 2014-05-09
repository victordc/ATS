using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    [MetadataType(typeof(SupervisorMetaData))]
    public partial class Supervisor
    {
        //public static IEnumerable<Supervisor> GetAllSupervisors()
        //{
        //    ATSCEEntities context = new ATSCEEntities();
        //    return context.Persons.OfType<Supervisor>().ToList();
        //}

        //public static Supervisor GetSupervisorById(int personId)
        //{
        //    ATSCEEntities context = new ATSCEEntities();
        //    IQueryable<Supervisor> query = from s in context.Persons.OfType<Supervisor>()
        //                              where s.PersonId == personId
        //                              select s;
        //    Supervisor supervisor = query.FirstOrDefault<Supervisor>();
        //    return supervisor;
        //}
    }

    public class SupervisorMetaData
    {
        //TODO make sure whether supervisorId is required
        //[Required(ErrorMessage = "Client Company is Required")]
        [DisplayName("Client Company")]
        public int CompanyId { get; set; }
    }
}
