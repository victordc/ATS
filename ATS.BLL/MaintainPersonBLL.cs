﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.Data.DAL;
using ATS.Data.Model;

namespace ATS.BLL
{
    
    public class MaintainPersonBLL
    {
        private IPersonRepository repository;

        public MaintainPersonBLL()
        {
            this.repository = new PersonRepository();
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
            return null;
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

        public IEnumerable<Supervisor> GetSupervisors()
        {
            return repository.GetSupervisors();
        }

        public IEnumerable<Supervisor> GetSupervisorsByCompany(int companyId)
        {
            //If has company repository
            //Company company = companyRepository.GetCompanyById(companyId);
            Company company = new Company();
            return repository.GetSupervisorsByCompany(company);

        }


        public void AssignStaffToSupervisor(Supervisor supervisor, IEnumerable<Staff> staffs)
        {
            //Biz Rule: Overrite the supervisor if has supervisor alryd?
        }

        #endregion

        #region Agent
        public Agent GetAgentById(int agentId)
        {
            return repository.GetAgentByID(agentId);
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

        public void AssignStaffsToAgent(Agent agent, IEnumerable<Staff> staffs)
        {
            //TODO, check Biz Rule for Staff Assignement
            //overwrite agentId or show warning
        }

        public IEnumerable<Agent> GetAgents()
        {
            return repository.GetAgents();
        }

        
        

        #endregion

    }
}