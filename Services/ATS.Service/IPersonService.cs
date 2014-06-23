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
        [FaultContract(typeof(ATSFault))]
        PersonDTO GetPerson(int personId);
       
        [OperationContract]
        [FaultContract(typeof(ATSFault))]
        PersonDTO[] GetSupervisedStaffs(int companyId);
    }

    [DataContract]
    public class ATSFault
    {
        private string errorCode;
        private string errorMessage;

        [DataMember]
        public string ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }

        public String ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

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

        [DataMember]
        public string SupervisorName { get; set; }
        
    }

    [DataContract(Name = "Agent")]
    public class AgentDTO : PersonDTO
    {
        [DataMember]
        public string Role { get; set; }
    }
}