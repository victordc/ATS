using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.Data.DAL;
using ATS.Data.Model;

namespace ATS.BLL
{
    
    public class MaintainPersonFacade
    {
        private IPersonRepository repository;

        public MaintainPersonFacade()
        {
            this.repository = new PersonRepository();
        }

        public IEnumerable<Person> GetAll()
        {
            return repository.GetAll();
        }

        public Person GetPersonById(int personId)
        {
            return repository.GetPersonById(personId);
        }

      

        public bool IsUniqueUsername(Person person)
        {
            return repository.IsUniqueUsername(person);
        }

        public bool IsUniqueEmail(Person person)
        {
            return repository.IsUniqueEmail(person);
        }

        #region Staff
        public void InsertStaff(Staff staff)
        {
            repository.InsertStaff(staff);
        }

        public void UpdateStaff(Staff staff)
        {
            repository.UpdateStaff(staff);
        }

        public void DeleteStaff(int staffId)
        {
            //TODO, confirm whether delete from db or just update status?
            Staff staff = repository.GetStaffByID(staffId);
            //change staff status to invisble?
            //save staff changes
        }

        public Staff GetStaffById(int staffId)
        {
            return repository.GetStaffByID(staffId);
        }

        public Staff GetStaffByUsername(string username)
        {
            return repository.GetStaffByUsername(username);
        }

        public IEnumerable<Staff> GetStaffs()
        {
            return repository.GetStaffs();
        }


        public IEnumerable<Staff> GetUnsupervisedStaffs()
        {
            return repository.GetUnsupervisedStaffs();
        }

        public IEnumerable<Staff> GetSupervisedStaffs(Supervisor supervisor)
        {
            return repository.GetStaffsBySupervisor(supervisor.PersonId);
        }

        public IEnumerable<Staff> GetRepresentedStaffs(Agent agent)
        {
            return repository.GetStaffsByAgent(agent.PersonId);
        }

        public IEnumerable<Staff> GetStaffsWithoutAgency()
        {
            return repository.GetStaffWithoutAgent();
        }


        #endregion

        #region Supervisor

        public void InsertSupervisor(Supervisor supervisor)
        {
            repository.InsertSupervisor(supervisor);
        }

        public void UpdateSupervisor(Supervisor supervisor)
        {
            repository.UpdateSupervisor(supervisor);
        }

        public void DeleteSupervisor(int supervisorId)
        {
            //TODO, check whether really delete?
            repository.DeleteSupervisor(supervisorId);
        }

        public Supervisor GetSupervisorById(int supervisorId)
        {
            return repository.GetSupervisorByID(supervisorId);
        }

        public Supervisor GetSupervisorByUsername(String username)
        {
            return repository.GetSupervisorByUsername(username);
        }

        public IEnumerable<Supervisor> GetSupervisors()
        {
            return repository.GetSupervisors();
        }

        public IEnumerable<Supervisor> GetSupervisorsByCompany(Company company)
        {
            return repository.GetSupervisorsByCompany(company);

        }

        public void AssignStaffsUnderSupervisor(Supervisor supervisor, int[] staffIds)
        {
            //We should use bulk update, but for this CA, we use this non-efficient way
            foreach (int staffId in staffIds)
            {
                Staff staff = repository.GetStaffByID(staffId);
                staff.SupervisorId = supervisor.PersonId;
                repository.UpdateStaff(staff);
            }
        }

        public void RemoveStaffsFromSupervisor(Supervisor supervisor, int[] staffIds)
        {
            //We should use bulk update, but for this CA, we use this non-efficient way
            foreach (int staffId in staffIds)
            {
                Staff staff = repository.GetStaffByID(staffId);
                staff.SupervisorId = null;
                repository.UpdateStaff(staff);
            }
        }


        #endregion

        #region Agent
        public Agent GetAgentById(int agentId)
        {
            return repository.GetAgentByID(agentId);
        }

        public Agent GetAgentByUsername(string username)
        {
            return repository.GetAgentByUsername(username);
        }

        public void InsertAgent(Agent agent)
        {
            repository.InsertAgent(agent);
        }

        public void UpdateAgent(Agent agent)
        {
            repository.UpdateAgent(agent);
        }

        public void DeleteAgent(int agentId)
        {
            //TODO check Biz Rule for deletion
            repository.DeleteAgent(agentId);
        }

        
        public IEnumerable<Agent> GetAgents()
        {
            return repository.GetAgents();
        }

        public void AssignStaffsUnderAgent(Agent agent, int[] staffIds)
        {
            //We should use bulk update, but for this CA, we use this non-efficient way
            foreach (int staffId in staffIds)
            {
                Staff staff = repository.GetStaffByID(staffId);
                staff.AgentId = agent.PersonId;
                repository.UpdateStaff(staff);
            }
        }

        

        public void RemoveStaffsFromAgent(Agent agent, int[] staffIds)
        {
            //We should use bulk update, but for this CA, we use this non-efficient way
            foreach (int staffId in staffIds)
            {
                Staff staff = repository.GetStaffByID(staffId);
                staff.AgentId = null;
                repository.UpdateStaff(staff);
            }
        }
        

        #endregion

    }
}
