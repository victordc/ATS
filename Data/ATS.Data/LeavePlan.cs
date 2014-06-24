using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using ATS.Data;

namespace ATS.Data.Model
{
    [MetadataType(typeof(LeavePlanData))]
    public partial class LeavePlan
    {

        public static LeavePlan Save(LeavePlan leavePlan)
        {
            string result = string.Empty;
            try
            {
                ATSCEEntities context = new ATSCEEntities();
                if (leavePlan.LeavePlanId <= 0)
                {
                    context.LeavePlans.Add(leavePlan);
                }
                else
                {
                    context.Entry(leavePlan).State = System.Data.EntityState.Modified;
                }
                int rowAffected = context.SaveChanges();

                if (rowAffected <= 0)
                    result = "Record is not saved";
                return leavePlan;
            }
            catch (Exception ex)
            {
                throw new Exception("Record is not saved [" + ex.Message + "]");
            }
        }

        public static LeavePlan GetById(int leavePlanId)
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.LeavePlans.Find(leavePlanId);

        }

        public static IEnumerable<LeavePlan> GetAll()
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.LeavePlans.Include(l => l.LeaveCategory).Include(l => l.Person);
        }


        public static IEnumerable<LeavePlan> GetAll(int userId)
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.LeavePlans.Include(l => l.LeaveCategory).Include(l => l.Person).Where(l => l.PersonId == userId);
        }

        public static IEnumerable<LeavePlan> GetAll(int userId, int year)
        {
            ATSCEEntities context = new ATSCEEntities();
            DateTime yearStart = new DateTime(year, 1, 1);
            DateTime yearEnd = new DateTime(year, 12, 31);
            return context.LeavePlans.Include(l => l.LeaveCategory).Include(l => l.Person)
                .Where(l => l.PersonId == userId && l.StartDate <= yearEnd && l.EndDate >= yearStart);
        }

        public static IEnumerable<LeavePlan> GetAll(int userId, int year, int month)
        {
            ATSCEEntities context = new ATSCEEntities();
            DateTime monthStart = new DateTime(year, month, 1);
            DateTime monthEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return context.LeavePlans.Include(l => l.LeaveCategory).Include(l => l.Person)
                .Where(l => l.PersonId == userId && l.StartDate <= monthEnd && l.EndDate >= monthStart);
        }
        
        public static IEnumerable<LeavePlan> GetAllLeavePlansForTeam(int userId)
        {
            ATSCEEntities context = new ATSCEEntities();
            IEnumerable<LeavePlan> leavesForTeam = null;
            int queryId = userId;
            //Check if this user has Supervisor
            var query = ((from persons in context.Persons
                          where persons.PersonId == userId
                          select persons).FirstOrDefault() as Staff);
            if (query != null)
            {
                queryId = query.SupervisorId.Value;
            }
            leavesForTeam = from allLeavePlans in context.LeavePlans
                            where (allLeavePlans.Person as Staff).SupervisorId == queryId
                            select allLeavePlans;
            return leavesForTeam;
        }

        public static IEnumerable<LeavePlan> GetAllLeavePlansForTeam(int userId, int year, int month)
        {
            ATSCEEntities context = new ATSCEEntities();
            IEnumerable<LeavePlan> leavesForTeam = null;
            DateTime monthStart = new DateTime(year, month, 1);
            DateTime monthEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            int queryId = userId;
            //Check if this user has Supervisor
            var query = ((from persons in context.Persons
                          where persons.PersonId == userId
                          select persons).FirstOrDefault() as Staff);
            if (query != null)
            {
                queryId = query.SupervisorId.Value;
            }
            leavesForTeam = from allLeavePlans in context.LeavePlans
                            where (allLeavePlans.Person as Staff).SupervisorId == queryId
                            where (allLeavePlans.StartDate <= monthEnd && allLeavePlans.EndDate >= monthStart)
                            select allLeavePlans;
            return leavesForTeam;
        }

        public static bool Delete(int leavePlanId)
        {
            try
            {
                ATSCEEntities context = new ATSCEEntities();
                LeavePlan leaveplan = context.LeavePlans.Find(leavePlanId);
                context.LeavePlans.Remove(leaveplan);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static IEnumerable<LeavePlan> GetAllLeavePlansBySupervisorId(int id)
        {
            ATSCEEntities context = new ATSCEEntities();
            IEnumerable<LeavePlan> leavesToSupervise = from allLeavePlans in context.LeavePlans
                                                       where (allLeavePlans.Person as Staff).SupervisorId == id
                                                       where allLeavePlans.Admitted == null
                                                       select allLeavePlans;
            return leavesToSupervise;
        }

        public static IEnumerable<LeavePlan> AdmitOrRejectLeaves(int LeavePlanId, bool AdmitReject)
        {
            ATSCEEntities context = new ATSCEEntities();
            LeavePlan leaveToUpdate = context.LeavePlans.Where(lp => lp.LeavePlanId == LeavePlanId).FirstOrDefault();
            if (AdmitReject)
            {
                leaveToUpdate.Admitted = true;
            }
            else
            {
                leaveToUpdate.Admitted = false;
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not admit or reject [" + ex.Message + "]");
            }

            return GetAllLeavePlansBySupervisorId(int.Parse((leaveToUpdate.Person as Staff).SupervisorId.ToString()));
        }

        public static bool CheckOverLap(LeavePlan leave)
        {
            ATSCEEntities context = new ATSCEEntities();
            IEnumerable<LeavePlan> AllLeaves = from leaves in context.LeavePlans
                                               where ((leaves.PersonId == leave.PersonId) && ((leaves.Admitted == null) || (leaves.Admitted == true)))
                                               select leaves;
            Console.WriteLine(AllLeaves.Count());
            foreach (LeavePlan lp in AllLeaves)
            {
                if (lp.LeavePlanId != leave.LeavePlanId)
                {
                    bool startBetween = (leave.StartDate >= lp.StartDate && leave.StartDate <= lp.EndDate);
                    bool endBetween = (leave.EndDate >= lp.StartDate && leave.EndDate <= lp.EndDate);
                    if (startBetween || endBetween)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    public class LeavePlanData
    {
        //[Required(ErrorMessage = "Leave Category is required")]
        //[DisplayName("Leave Category")]
        //public int LeaveCategoty { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DisplayName("From")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        //[DataType(DataType.Date)]
        [DisplayName("To")]
        public DateTime EndDate { get; set; }


        [Required(ErrorMessage = "Duration is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Leave duration should be at least 1 day.")]
        [DisplayName("Duration")]
        public string Duration { get; set; }

    }
}


