using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.BLL;
using ATS.Data;
using ATS.Data.DAL;
using ATS.Data.Model;

namespace ATS.Service
{
    public class TimesheetService : ITimesheetService
    {
        private MaintainPersonFacade personFacade = new MaintainPersonFacade();
        private MaintainCompanyFacade companyFacade = new MaintainCompanyFacade();

        public TimesheetSummary[] GetTimesheetSummary(int companyId, int year, int month)
        {
            List<TimesheetSummary> result = new List<TimesheetSummary>();
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
                    TimesheetSummary summary = new TimesheetSummary();
                    summary.StaffName = staff.PersonName;
                    summary.SupervisorName = (staff.Supervisor != null) ? staff.Supervisor.PersonName : "";
                    summary.AgentInCharge = (staff.Agent != null) ? staff.Agent.PersonName : "";
                    summary.WorkingHours = 0;
                    
                    //find Master
                    TimeSheetMaster master = TimeSheetMasterRepository.GetTimeSheetsByPersonId(staff.PersonId, year, month);
                    if (master != null)
                    {
                        switch (master.Status)
                        {
                            case (int)TimeSheetStatus.Approved: summary.Status = "Approved"; break;
                            case (int)TimeSheetStatus.Draft: summary.Status = "Draft"; break;
                            case (int)TimeSheetStatus.Rejected: summary.Status = "Rejected"; break;
                            case (int)TimeSheetStatus.Submitted: summary.Status = "Pending"; break;
                            default: summary.Status = "Pending"; break;

                        }
                        IEnumerable<TimeSheetDetail> details = TimesheetRepository.GetAllTimeSheetDetailsByMaster(master);

                        foreach (var detail in details)
                        {
                            summary.WorkingHours += detail.Hour;
                        }


                    }
                    else
                    {
                        summary.Status = "N.A";
                    }
                    result.Add(summary);
                }

            }
            return result.ToArray();
        }

        public TimeSheetDTO[] GetTimesheetDetail(int personId, int year, int month)
        {
            List<TimeSheetDTO> result = new List<TimeSheetDTO>();
            Staff staff = personFacade.GetStaffById(personId);
            if (staff != null)
            {
                //find Master
                TimeSheetMaster master = TimeSheetMasterRepository.GetTimeSheetsByPersonId(staff.PersonId, year, month);
                if (master != null)
                {

                    IEnumerable<TimeSheetDetail> details = TimesheetRepository.GetAllTimeSheetDetailsByMaster(master);

                    foreach (var detail in details)
                    {
                        TimeSheetDTO dto = new TimeSheetDTO();
                        dto.StartTime = detail.StartTime;
                        dto.EndTime = detail.EndTime;
                        dto.Hour = detail.Hour;
                        result.Add(dto);
                    }
                }
            }
            return result.ToArray();

        }

    }
}
