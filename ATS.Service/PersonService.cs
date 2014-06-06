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

        public MaintainPersonFacade personFacade = new MaintainPersonFacade();

        public PersonDTO GetPerson(int personId)
        {
            Staff staff = personFacade.GetStaffById(personId);
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
                Agent agent = personFacade.GetAgentById(personId);
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
            Supervisor supervisor = personFacade.GetSupervisorById(supervisorId);
            if (supervisor == null)
            {
                //return soap exception??
                return null; 
            }
            IEnumerable<Staff> staffs = personFacade.GetSupervisedStaffs(supervisor);
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
