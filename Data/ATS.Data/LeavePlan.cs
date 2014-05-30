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

        public static IEnumerable<LeavePlan> GetAll(int userId)
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.LeavePlans.Include(l => l.LeaveCategory).Include(l => l.Person).Where(l=>l.PersonId == userId);
        }

        public static IEnumerable<LeavePlan> GetAll()
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.LeavePlans.Include(l => l.LeaveCategory).Include(l => l.Person);
        }

        public static IEnumerable<LeavePlan> GetAllLeavePlansForTeam(int userId)
        {
            ATSCEEntities context = new ATSCEEntities();
            IEnumerable<LeavePlan> leaves = from allLeavePlans in context.LeavePlans
                                            where (allLeavePlans.Person as Staff).SupervisorId == userId
                                            select allLeavePlans;
            return leaves;
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

    }

    public class LeavePlanData
    {
        //[Required(ErrorMessage = "Leave Category is required")]
        //[DisplayName("Leave Category")]
        //public int LeaveCategoty { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        [DisplayName("From")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        [DisplayName("To")]
        public DateTime EndDate { get; set; }


        [Required(ErrorMessage = "Duration is required")]
        [DisplayName("Duration")]
        public string Duration { get; set; }

    }
}


