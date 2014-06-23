using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service
{
    [ServiceContract]
    public interface ITimesheetService
    {
        
        [OperationContract]
        TimesheetSummary[] GetTimesheetSummary(int companyId, int year, int month);
        [OperationContract]
        TimeSheetDTO[] GetTimesheetDetail(int personId, int year, int month);
    }

    [DataContract]
    public class TimesheetSummary
    {
        [DataMember]
        public string StaffName { get; set; }
        [DataMember]
        public string SupervisorName { get; set; }
        [DataMember]
        public string AgentInCharge { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public double WorkingHours {get; set;}

    }

    [DataContract(Name="TimeSheet")]
    public class TimeSheetDTO
    {
        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public DateTime EndTime { get; set; }
        [DataMember]
        public double Hour { get; set; }
        
    }
}
