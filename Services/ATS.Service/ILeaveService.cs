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
    public interface ILeaveService
    {
        [OperationContract]
        LeaveSummary[] GetLeaveSummary(int companyId, int year, int month);

        [OperationContract]
        LeaveDTO[] GetLeaveDetails(int personId, int year, int month);

    }

    [DataContract]
    public class LeaveSummary
    {
        [DataMember]
        public int StaffID { get; set; }
        [DataMember]
        public string StaffName { get; set; }
        [DataMember]
        public double ApprovedDuration { get; set; }
        [DataMember]
        public double RejectDuration { get; set; }
        [DataMember]
        public double PendingDuration { get; set; }
        [DataMember]
        public double TotalDuration { get; set; }
    }

    [DataContract(Name="Leave")]
    public class LeaveDTO
    {
        [DataMember]
        public String LeaveCategory {get; set;}
        [DataMember]
        public DateTime FromDate {get; set;}
        [DataMember]
        public DateTime ToDate {get; set;}
        [DataMember]
        public double Duration { get; set; }
    }
}
