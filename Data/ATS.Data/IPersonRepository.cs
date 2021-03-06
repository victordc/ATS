﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Data.Model;

namespace ATS.Data.DAL
{
    public interface IPersonRepository : IDisposable
    {
        IEnumerable<Person> GetAll();
        Person GetPersonById(int personId);
        bool IsUniqueUsername(Person person);
        bool IsUniqueEmail(Person person);

        IEnumerable<Staff> GetStaffs();
        Staff GetStaffByID(int personId);
        Staff GetStaffByUsername(string username);
        void InsertStaff(Staff staff);
        bool DeleteStaff(int personId);
        void UpdateStaff(Staff staff);
        IEnumerable<Staff> GetStaffsBySupervisor(int supervisorId);
        IEnumerable<Staff> GetUnsupervisedStaffs();
        IEnumerable<Staff> GetStaffsByAgent(int agentId);
        IEnumerable<Staff> GetStaffWithoutAgent();
        

        IEnumerable<Supervisor> GetSupervisors();
        Supervisor GetSupervisorByID(int personId);
        Supervisor GetSupervisorByUsername(string username);
        void InsertSupervisor(Supervisor supervisorId);
        bool DeleteSupervisor(int personId);
        void UpdateSupervisor(Supervisor supervisorId);
        IEnumerable<Supervisor> GetSupervisorsByCompany(Company company);

        IEnumerable<Agent> GetAgents();
        Agent GetAgentByID(int personId);
        Agent GetAgentByUsername(string username);
        void InsertAgent(Agent agent);
        bool DeleteAgent(int personId);
        void UpdateAgent(Agent agent);
    }
}
