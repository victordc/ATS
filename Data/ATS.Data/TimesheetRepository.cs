using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ATS.Data
{
    public class TimesheetRepository
    {
        #region Singleton

        private static TimesheetRepository _instance;

        public static TimesheetRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimesheetRepository();
                }

                return _instance;
            }
        }

        #endregion

        #region Membership

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserProfile> GetAllUsers()
        {
            return UserProfile.GetAllUsers();
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <returns></returns>
        public UserProfile GetUserById(int id)
        {
            return UserProfile.GetUserById(id);
        }

        /// <summary>
        /// Gets users by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<UserProfile> GetUsersByName(string name)
        {
            return UserProfile.GetUsersByName(name);
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<webpages_Roles> GetAllRoles()
        {
            return webpages_Roles.GetAllRoles();
        }

        /// <summary>
        /// Get role by name 
        /// </summary>
        /// <returns></returns>
        public webpages_Roles GetRoleByName(string name)
        {
            return webpages_Roles.GetRoleByName(name);
        }

        #endregion

        #region LeavePlan

        //--------------------------------------------------------------------------------
        public static LeavePlan GetLeavePlanById(int leavePlanId)
        {
            return LeavePlan.GetById(leavePlanId);
        }

        public static IEnumerable<LeavePlan> GetLeavePlans()
        {
            return LeavePlan.GetAll();
        }

        public static IEnumerable<LeavePlan> GetLeavePlans(int userId)
        {
            return LeavePlan.GetAll(userId);
        }

        public static IEnumerable<LeavePlan> GetLeavePlans(int userId, int year)
        {
            return LeavePlan.GetAll(userId, year);
        }

        public static IEnumerable<LeavePlan> GetAdmittedLeavePlans(int userId, int year)
        {
            return LeavePlan.GetAllAdmitted(userId, year);
        }

        public static IEnumerable<LeavePlan> GetLeavePlans(int userId, int year, int month)
        {
            return LeavePlan.GetAll(userId, year, month);
        }


        public static IEnumerable<LeavePlan> GetLeavePlansForTeam(int userId)
        {
            return LeavePlan.GetAllLeavePlansForTeam(userId);
        }

        public static IEnumerable<LeavePlan> GetLeavePlansForTeam(int userId, int year, int month)
        {
            return LeavePlan.GetAllLeavePlansForTeam(userId, year, month);
        }

        public static LeavePlan AddUpdateLeavePlan(LeavePlan leavePlan)
        {
            return LeavePlan.Save(leavePlan);
        }

        public static bool CheckLeavesOverlaps(LeavePlan leavePlan)
        {
            return LeavePlan.CheckOverLap(leavePlan);
        }

        public static bool RemoveLeavePlan(int leavePlanId)
        {
            return LeavePlan.Delete(leavePlanId);
        }

        public static IEnumerable<LeaveCategory> GetLeaveCategories()
        {
            return LeaveCategory.getAll();
        }

        public static IEnumerable<LeavePlan> GetAllPersonsBySupervisorId(int SupervisorId)
        {
            return LeavePlan.GetAllLeavePlansBySupervisorId(SupervisorId);
        }

        public static IEnumerable<LeavePlan> AdmitReject(int LeavePlanId, bool AdmitReject)
        {
            return LeavePlan.AdmitOrRejectLeaves(LeavePlanId, AdmitReject);
        }

        public static LeaveCategory getLeaveCategoryById(int id)
        {
            return LeaveCategory.getById(id);
        }
        public static int getTotalRemainingLeaveDays(LeavePlan leaveplan)
        {
            return LeavePlan.getTotalRemainingLeaveDays(leaveplan);
        }
        //--------------------------------------------------------------------------------
        #endregion

        #region Person

        public static Person GetPersonById(int PersonId)
        {
            return Person.GetById(PersonId);
        }

        public static IEnumerable<Person> GetAllPersons()
        {
            return Person.GetAll();
        }

        //----------------------------------------------------------------------------------

        public static IEnumerable<Person> GetAllSupervisors()
        {
            return Supervisor.GetAll();
        }

        public static IEnumerable<Company> GetAllCompanies()
        {
            return Company.GetAll();
        }

        public Person GetPersonByUserName(string userName)
        {
            return Person.GetByName(userName);
        }

        #endregion

        #region Timesheet

        //----------------------------------------------------------------------------------

        public static TimeSheetDetail GetTimeSheetDetailById(int TimeSheetDetailId)
        {
            return TimeSheetDetail.GetById(TimeSheetDetailId);
        }

        public static IEnumerable<TimeSheetDetail> GetAllTimeSheetDetails()
        {
            return TimeSheetDetail.GetAll();
        }

        public static IEnumerable<TimeSheetDetail> GetAllTimeSheetDetailsByMaster(TimeSheetMaster master)
        {
            return TimeSheetDetail.GetAllTimeSheetDetailsByMaster(master);
        }

        //----------------------------------------------------------------------------------

        public static TimeSheetMaster GetTimeSheetMasterById(int TimeSheetMasterId)
        {
            return TimeSheetMaster.GetById(TimeSheetMasterId);
        }

        public static IEnumerable<TimeSheetMaster> GetAllTimeSheetMasters()
        {
            return TimeSheetMaster.GetAll();
        }

        #endregion

        #region Setup Company

        /// <summary>
        /// Setup company
        /// </summary>
        /// <param name="setupCompany"></param>
        /// <returns></returns>
        public int SetupCompany(SetupCompany setupCompany)
        {
            int result = -1;
            try
            {
                //using (TransactionScope scope = new TransactionScope())
                {
                    //1. Insert company
                    setupCompany.Company.Save();

                    //2. Insert supervisors
                    foreach (RegisterModel item in setupCompany.Supervisors)
                    {
                        Supervisor supervisor = new Supervisor();
                        supervisor.PersonName = item.FullName;
                        supervisor.UserName = item.UserName;

                        supervisor.Save();
                    }

                    //3. Insert Agents
                    foreach (RegisterModel item in setupCompany.Agents)
                    {
                        Agent agent = new Agent();
                        agent.PersonName = item.FullName;
                        agent.UserName = item.UserName;
                        agent.Save();
                    }

                    //4. Insert Staffs
                    foreach (RegisterModel item in setupCompany.Staffs)
                    {
                        Staff staff = new Staff();
                        staff.PersonName = item.FullName;
                        staff.UserName = item.UserName;
                        staff.SupervisorId = Person.GetByName(item.SupervisorName).PersonId;
                        staff.AgentId = Person.GetByName(item.AgentName).PersonId;
                        staff.Save();
                    }

                    //scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion

    }
}
