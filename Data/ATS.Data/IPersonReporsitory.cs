using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Data.Model;

namespace ATS.Data.DAL
{
    public interface IPersonRepository : IDisposable
    {
        IEnumerable<Staff> GetStaffs();
        Staff GetStaffByID(int personId);
        void InsertStaff(Staff staff);
        bool DeleteStaff(int personId);
        void UpdateStaff(Staff staff);

        IEnumerable<Supervisor> GetSupervisors();
        Supervisor GetSupervisorByID(int personId);
        void InsertSupervisor(Supervisor supervisorId);
        bool DeleteSupervisor(int personId);
        void UpdateSupervisor(Supervisor supervisorId);

        IEnumerable<Agent> GetAgents();
        Agent GetAgentByID(int personId);
        void InsertAgent(Agent agent);
        bool DeleteAgent(int personId);
        void UpdateAgent(Agent agent);
    }
}
