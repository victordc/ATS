using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ATS.Service
{
    [ServiceContract]
    interface IPersonService
    {
        [OperationContract]
        string GetPerson(int personId);
    }

    [DataContract(Name="Person")]
    public class PersonDTO
    {
        [DataMember]
        public int PersonId { get; set; }

        [DataMember]
        public String PersonName { get; set; }

        
    }

    [DataContract(Name="Staff")]
    public class StaffDTO : PersonDTO
    {
        
    }

    [DataContract(Name = "Agent")]
    public class AgentDTO : PersonDTO
    {
    }
}
