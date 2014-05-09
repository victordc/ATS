using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ATS.Data.Model
{
    //TODO check whether to make this class abstract

    [MetadataType(typeof(PersonData))]
    public partial class Person
    {

        public static Person GetById(int PersonId)
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.Persons.Find(PersonId);
        }



        public static IEnumerable<Person> GetAll()
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.Persons;
        }

        

       

        //public static Staff AssignAsStaff(Person p, Supervisor s, Agent a)
        //{
        //    ATSCEEntities context = new ATSCEEntities();
        //    Staff staff = (Staff) p;
        //    staff.AgentId = a.PersonId;
        //    staff.SupervisorId = s.PersonId;
        //    context.SaveChanges();
        //    return staff;
        //}

        //public static Supervisor AssignAsSupervisor(Person p, Company c, Agent a)
        //{
        //    ATSCEEntities context = new ATSCEEntities();
        //    Supervisor s = (Supervisor)p;
        //    s.AgentId = a.PersonId;
        //    s.CompanyId = c.CompanyId;
        //    context.SaveChanges();
        //    return s;
        //}

        //public static Agent AssignAsAgent(Person p, Supervisor s)
        //{
        //    ATSCEEntities context = new ATSCEEntities();
        //    Agent a = (Agent)p;
        //    a.SupervisorId = s.PersonId;
        //    context.SaveChanges();
        //    return a;
        //}
        
    }

    public class PersonData
    {
        
        [DisplayName("Name")]
        public string PersonName { get; set; }

        //[DisplayName("Category ID")]
        //public int PersonCategoryId { get; set; }

        [DisplayName("Phone")]
        public string Phone { get; set; }

        
        [DisplayName("Home Address")]
        public string HomeAddress { get; set; }
    }
}
