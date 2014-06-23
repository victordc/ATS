using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ATS.BLL;
using ATS.Data.Model;

namespace ATS.Service
{
    
    class PersonService : IPersonService
    {

        public MaintainPersonFacade personFacade = new MaintainPersonFacade();
        public MaintainCompanyFacade companyFacade = new MaintainCompanyFacade();

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
                if (staff.Supervisor != null) dto.SupervisorName = staff.Supervisor.PersonName;
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
                    adto.PersonId = staff.Agent.PersonId;
                    adto.Role = "Agent";
                    return adto;
                }
            }

            ATSFault notfound = new ATSFault();
            notfound.ErrorCode = "404";
            notfound.ErrorMessage = "No Staff/Agent with ID: " + personId;
            throw new FaultException<ATSFault>(notfound); 

        }

        public PersonDTO[] GetSupervisedStaffs(int companyId)
        {
            Company company = companyFacade.GetCompanyById(companyId);
            if (company == null)
            {
                ATSFault notfound = new ATSFault();
                notfound.ErrorCode = "404";
                notfound.ErrorMessage = "Company With ID: " + companyId + " not existed";
                throw new FaultException<ATSFault>(notfound); 
            }
            ICollection<StaffDTO> result = new List<StaffDTO>();
            IEnumerable<Supervisor> supervisors = personFacade.GetSupervisorsByCompany(company);
            foreach (var supervisor in supervisors)
            {
                
                IEnumerable<Staff> staffs = personFacade.GetSupervisedStaffs(supervisor);
                
                foreach (var staff in staffs)
                {
                    StaffDTO dto = new StaffDTO();
                    dto.PersonId = staff.PersonId;
                    dto.PersonName = staff.PersonName;
                    dto.HomeAddress = staff.HomeAddress;
                    dto.Phone = staff.Phone;
                    dto.SupervisorName = supervisor.PersonName;
                    if (staff.Agent != null)
                    {
                        AgentDTO adto = new AgentDTO();
                        adto.HomeAddress = staff.Agent.HomeAddress;
                        adto.Phone = staff.Agent.Phone;
                        adto.PersonName = staff.Agent.PersonName;
                        adto.PersonId = staff.Agent.PersonId;
                        adto.Role = "Agent";
                        dto.Agent = adto;
                    }
                    result.Add(dto);
                }
                
            }
            
            return result.ToArray(); ;
        }


        
        
    }
}
