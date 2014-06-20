using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ATS.Service
{
    [ServiceContract]
    public interface IPersonService
    {
        
        [OperationContract]
        PersonDTO GetPerson(int personId);

       
        [OperationContract]
        PersonDTO[] GetSupervisedStaffs(int supervisorId);
    }

    [DataContract(Name="Person")]
    [KnownType(typeof(StaffDTO))]
    [KnownType(typeof(AgentDTO))]
    public class PersonDTO
    {
        [DataMember]
        public int PersonId { get; set; }

        [DataMember]
        public String PersonName { get; set; }

        [DataMember]
        public String Phone { get; set; }

        [DataMember]
        public String HomeAddress { get; set; }
    }

    [DataContract(Name="Staff")]
    public class StaffDTO : PersonDTO
    {
        [DataMember]
        public AgentDTO Agent { get; set; }
        
    }

    [DataContract(Name = "Agent")]
    public class AgentDTO : PersonDTO
    {
        [DataMember]
        public string Role { get; set; }
    }
}