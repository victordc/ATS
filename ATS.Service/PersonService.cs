using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BLL;
using ATS.Data.Model;

namespace ATS.Service
{
    
    class PersonService : IPersonService
    {

        public MaintainPersonBLL personBll = new MaintainPersonBLL();

        public PersonDTO GetPerson(int personId)
        {
            Staff staff = personBll.GetStaffById(personId);
            if (staff != null)
            {
                StaffDTO dto = new StaffDTO();
                dto.PersonId = staff.PersonId;
                dto.PersonName = staff.PersonName;
                dto.HomeAddress = staff.HomeAddress;
                dto.Phone = staff.Phone;
                if(staff.Agent != null){
                    AgentDTO adto = new AgentDTO();
                    adto.HomeAddress = staff.Agent.HomeAddress;
                    adto.Phone = staff.Agent.Phone;
                    adto.PersonName = staff.Agent.PersonName;
                    adto.PersonId = staff.PersonId;
                    adto.Role = "Agent";
                    dto.Agent = adto;
                }
                return dto;
            }
            else
            {
                //Not staff, Agent
                Agent agent = personBll.GetAgentById(personId);
                if(agent != null){
                    AgentDTO adto = new AgentDTO();
                    adto.HomeAddress = staff.Agent.HomeAddress;
                    adto.Phone = staff.Agent.Phone;
                    adto.PersonName = staff.Agent.PersonName;
                    adto.PersonId = staff.PersonId;
                    adto.Role = "Agent";
                    return adto;
                }
            }

            return null;
            
        }

        public PersonDTO[] GetSupervisedStaffs(int supervisorId)
        {
            Supervisor supervisor = personBll.GetSupervisorById(supervisorId);
            if (supervisor == null)
            {
                //return soap exception??
                return null; 
            }
            IEnumerable<Staff> staffs = personBll.GetSupervisedStaffs(supervisor);
            ICollection<StaffDTO> result = new List<StaffDTO>();
            foreach (var staff in staffs)
            {
                StaffDTO dto = new StaffDTO();
                dto.PersonId = staff.PersonId;
                dto.PersonName = staff.PersonName;
                dto.HomeAddress = staff.HomeAddress;
                dto.Phone = staff.Phone;
                if (staff.Agent != null)
                {
                    AgentDTO adto = new AgentDTO();
                    adto.HomeAddress = staff.Agent.HomeAddress;
                    adto.Phone = staff.Agent.Phone;
                    adto.PersonName = staff.Agent.PersonName;
                    adto.PersonId = staff.PersonId;
                    adto.Role = "Agent";
                    dto.Agent = adto;
                }
                result.Add(dto);
            }

            return result.ToArray(); ;
        }


        
        
    }
}
