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
        public string Save()
        {
            string result = string.Empty;
            try
            {
                using (var context = new ATSCEEntities())
                {
                    if (this.PersonId <= 0)
                    {
                        context.Persons.Add(this);
                    }
                    else
                    {
                        context.Entry(this).State = System.Data.EntityState.Modified;
                    }
                    int rowAffected = context.SaveChanges();

                    if (rowAffected <= 0)
                        result = "Record is not saved";
                }
            }
            catch (Exception ex)
            {
                // Log Exception here.
                throw new Exception("Record is not saved: " + ex.ToString());
            }

            return result;
        }

        public static Person GetById(int PersonId)
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.Persons.Find(PersonId);
        }

        public static Person GetByName(string name)
        {
            ATSCEEntities context = new ATSCEEntities();
            //return context.Persons.Where(r=> r.PersonName == name).FirstOrDefault();
            return context.Persons.Where(r => r.UserName == name).FirstOrDefault();
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
        [Required]
        public string PersonName { get; set; }

        //[DisplayName("Category ID")]
        //public int PersonCategoryId { get; set; }

        [DisplayName("Phone")]
        [Required]
        public string Phone { get; set; }

        
        [DisplayName("Home Address")]
        public string HomeAddress { get; set; }

        //TODO, any Unique Validator ? use repository to validate for now
        [DisplayName("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddressAttribute]
        public string Email { get; set; }

        [DisplayName("Username")]
        [Required]
        public string UserName { get; set; }
    }
}
