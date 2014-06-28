using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Data.Model;

namespace ATS.Data.DAL
{
    public class PersonRepository : IPersonRepository, IDisposable
    {
        private ATSCEEntities context;

        public PersonRepository()
        {
            context = new ATSCEEntities();
        }

        public PersonRepository(ATSCEEntities context)
        {
            this.context = context;
        }

        public IEnumerable<Person> GetAll()
        {
            return context.Persons.ToList<Person>();
        }

        public Person GetPersonById(int personId)
        {
            IQueryable<Person> query = from s in context.Persons
                                      where s.PersonId == personId
                                      select s;
            Person person = query.FirstOrDefault<Person>();
            return person;
        }

        public bool IsUniqueUsername(Person person)
        {
            IQueryable<Person> query = from s in context.Persons
                                       where s.UserName == person.UserName
                                       select s;
           Person p = query.FirstOrDefault<Person>();
           if (p == null)
           {
               return true;
           }else 
                return false;
        }

        public bool IsUniqueEmail(Person person)
        {
            IQueryable<Person> query;
            if (person.PersonId != 0)
            {
                //Update
                query = from s in context.Persons
                    where s.Email == person.Email && s.PersonId != person.PersonId
                    select s;
            }
            else
            {
                //Create
                query = from s in context.Persons
                        where s.Email == person.Email
                        select s;
            }
            Person p = query.FirstOrDefault<Person>();
            if (p == null)
            {
                return true;
            }
            else
                return false;
        }

        public IEnumerable<Staff> GetStaffs()
        {
            return context.Persons.OfType<Staff>().ToList();
        }

        public Staff GetStaffByID(int personId)
        {
            IQueryable<Staff> query = from s in context.Persons.OfType<Staff>()
                                      where s.PersonId == personId
                                      select s;
            Staff staff = query.FirstOrDefault<Staff>();
            if (staff != null)
            {
                context.Entry(staff).Reference(c => c.Agent).Load();
                context.Entry(staff).Reference(c => c.Supervisor).Load();
            }
            return staff;
            
        }

        public Staff GetStaffByUsername(String username)
        {
            IQueryable<Staff> query = from s in context.Persons.OfType<Staff>()
                                      where s.UserName == username
                                      select s;
            Staff staff = query.FirstOrDefault<Staff>();
            return staff;

        }

        public void InsertStaff(Staff staff)
        {
            context.Persons.Add(staff);
            context.SaveChanges();
        }

        public bool DeleteStaff(int personId)
        {
            Staff staff = this.GetStaffByID(personId);
            if (staff != null)
            {
                context.Persons.Remove(staff);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void UpdateStaff(Staff staff)
        {
            context.Entry(staff).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Staff> GetStaffsBySupervisor(int supervisorId)
        {
            IQueryable<Staff> query = from s in context.Persons.Include("Agent").Include("Supervisor").OfType<Staff>()
                                           where s.SupervisorId == supervisorId
                                           select s;
            IEnumerable<Staff> staffs = query.ToList<Staff>();
            return staffs;
        }

        public IEnumerable<Staff> GetUnsupervisedStaffs()
        {
            IQueryable<Staff> query = from s in context.Persons.OfType<Staff>()
                                      where s.SupervisorId == null
                                      select s;
            IEnumerable<Staff> staffs = query.ToList<Staff>();
            return staffs;
        }

        public IEnumerable<Staff> GetStaffsByAgent(int agentId)
        {
            IQueryable<Staff> query = from s in context.Persons.OfType<Staff>()
                                      where s.AgentId == agentId 
                                      select s;
            IEnumerable<Staff> staffs = query.ToList<Staff>();
            return staffs;
        }

        public IEnumerable<Staff> GetStaffWithoutAgent()
        {
            IQueryable<Staff> query = from s in context.Persons.OfType<Staff>()
                                      where s.AgentId == null
                                      select s;
            IEnumerable<Staff> staffs = query.ToList<Staff>();
            return staffs;
        }


        public IEnumerable<Supervisor> GetSupervisors()
        {
            return context.Persons.OfType<Supervisor>().ToList();
        }

        public Supervisor GetSupervisorByID(int personId)
        {
            IQueryable<Supervisor> query = from s in context.Persons.OfType<Supervisor>()
                                      where s.PersonId == personId
                                      select s;
            Supervisor supervisor = query.FirstOrDefault<Supervisor>();
            return supervisor;
        }

        public Supervisor GetSupervisorByUsername(string username)
        {
            IQueryable<Supervisor> query = from s in context.Persons.OfType<Supervisor>()
                                           where s.UserName == username
                                           select s;
            Supervisor supervisor = query.FirstOrDefault<Supervisor>();
            return supervisor;
        }

        public IEnumerable<Supervisor> GetSupervisorsByCompany(Company company)
        {
            IQueryable<Supervisor> query = from s in context.Persons.OfType<Supervisor>()
                                           where s.CompanyId == company.CompanyId
                                           select s;
            IEnumerable<Supervisor> supervisors = query.ToList<Supervisor>();
            return supervisors;
        }

        public void InsertSupervisor(Supervisor supervisor)
        {
            context.Persons.Add(supervisor);
            context.SaveChanges();
        }

        public bool DeleteSupervisor(int personId)
        {
            Supervisor supervisor = this.GetSupervisorByID(personId);
            if (supervisor != null)
            {
                context.Persons.Remove(supervisor);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void UpdateSupervisor(Supervisor supervisor)
        {
            context.Entry(supervisor).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Agent> GetAgents()
        {
            return context.Persons.OfType<Agent>().ToList();
        }

        public Agent GetAgentByID(int personId)
        {
            IQueryable<Agent> query = from s in context.Persons.OfType<Agent>()
                                           where s.PersonId == personId
                                           select s;
            Agent agent = query.FirstOrDefault<Agent>();
            return agent;
        }

        public Agent GetAgentByUsername(string username)
        {
            IQueryable<Agent> query = from s in context.Persons.OfType<Agent>()
                                      where s.UserName == username
                                      select s;
            Agent agent = query.FirstOrDefault<Agent>();
            return agent;
        }

        public void InsertAgent(Agent agent)
        {
            context.Persons.Add(agent);
            context.SaveChanges();
        }

        public bool DeleteAgent(int personId)
        {
            Agent agent = this.GetAgentByID(personId);
            if (agent != null)
            {
                context.Persons.Remove(agent);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void UpdateAgent(Agent agent)
        {
            context.Entry(agent).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
