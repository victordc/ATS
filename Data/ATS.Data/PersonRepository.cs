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

        public PersonRepository(ATSCEEntities context)
        {
            this.context = context;
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
            context.Entry(staff).State = EntityState.Modified;
            context.SaveChanges();
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
            context.Entry(supervisor).State = EntityState.Modified;
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
            context.Entry(agent).State = EntityState.Modified;
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
