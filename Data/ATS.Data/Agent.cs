using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    [MetadataType(typeof(AgentMetadata))]
    public partial class Agent
    {
        //public static IEnumerable<Agent> GetAllAgents()
        //{
        //    ATSCEEntities context = new ATSCEEntities();
        //    return context.Persons.OfType<Agent>().ToList();
        //}

        //public static Agent GetStaffById(int personId)
        //{
        //    ATSCEEntities context = new ATSCEEntities();
        //    IQueryable<Agent> query = from s in context.Persons.OfType<Agent>()
        //                              where s.PersonId == personId
        //                              select s;
        //    Agent agent = query.FirstOrDefault<Agent>();
        //    return agent;
        //}
    }

    public class AgentMetadata
    {
        //TODO, must supervisorId required??
        //[Required(ErrorMessage = "Supervisor is required")]
        [DisplayName("Supervisor")]
        public int SupervisorId { get; set; }
    }
}
