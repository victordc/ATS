using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.BusinessFacade;
using ATS.Data;
using ATS.Data.Model;

namespace ATS.Service
{
    public class LeaveService : ILeaveService
    {
        private MaintainPersonFacade personFacade = new MaintainPersonFacade();
        private MaintainCompanyFacade companyFacade = new MaintainCompanyFacade();
        //private TimesheetRepository leaveRepo = new TimesheetRepository();

        public LeaveSummary[] GetLeaveSummary(int companyId, int year, int month)
        {
            List<LeaveSummary> result = new List<LeaveSummary>();
            Company company = companyFacade.GetCompanyById(companyId);
            if (company != null)
            {
                //find all the supervisors under this company
                IEnumerable<Supervisor> supervisors = personFacade.GetSupervisorsByCompany(company);
                List<Staff> staffs = new List<Staff>();
                foreach (var supervisor in supervisors)
                {
                    //add all supervised staffs to the list
                    IEnumerable<Staff> supervisedStaffs = personFacade.GetSupervisedStaffs(supervisor);
                    staffs.AddRange(supervisedStaffs);
                }
                //for each staff, calculate the leave summary
                foreach (var staff in staffs)
                {
                    LeaveSummary summary = new LeaveSummary();
                    summary.StaffName = staff.PersonName;
                    summary.ApprovedDuration = 0;
                    summary.RejectDuration = 0;
                    summary.PendingDuration = 0;
                    //find leaves
                    IEnumerable<LeavePlan> leavePlans = TimesheetRepository.GetLeavePlans(staff.PersonId, year, month);
                    foreach (var leavePlan in leavePlans)
                    {
                        if (leavePlan.Admitted == null)
                        {
                            //pending
                            summary.PendingDuration += leavePlan.Duration;
                        }
                        else if (leavePlan.Admitted == true)
                        {
                            //approved
                            summary.ApprovedDuration += leavePlan.Duration;

                        }
                        else
                        {
                            //rejected
                            summary.RejectDuration += leavePlan.Duration;
                        }
                    }
                    summary.TotalDuration = summary.ApprovedDuration + summary.PendingDuration + summary.RejectDuration;
                    result.Add(summary);
                }

            }

            return result.ToArray();
        }


        public LeaveDTO[] GetLeaveDetails(int personId, int year, int month)
        {
            List<LeaveDTO> result = new List<LeaveDTO>();
            Staff staff = personFacade.GetStaffById(personId);
            if (staff != null)
            {
                 IEnumerable<LeavePlan> leavePlans = TimesheetRepository.GetLeavePlans(staff.PersonId, year, month);
                 foreach (var leavePlan in leavePlans)
                 {
                     LeaveDTO dto = new LeaveDTO();
                     dto.Duration = leavePlan.Duration;
                     dto.FromDate = leavePlan.StartDate;
                     dto.ToDate = leavePlan.EndDate;
                     dto.LeaveCategory = leavePlan.LeaveCategory.LeaveCategoryDesc;
                     result.Add(dto);
                 }
            }
            return result.ToArray();
        }


    }
}
