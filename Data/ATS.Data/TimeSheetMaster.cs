using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Data.DAL;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web;

namespace ATS.Data.Model
{


    public partial class TimeSheetMaster
    {

        public IEnumerable<SelectListItem> ActionsList { get; set; }
        public virtual Agent Agent { get; set; }


        public static TimeSheetMaster GetById(int TimeSheetMasterId)
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.TimeSheetMasters.Find(TimeSheetMasterId);
        }

        public static IEnumerable<TimeSheetMaster> GetAll()
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.TimeSheetMasters;
        }

        public static IEnumerable<TimeSheetMaster> GetAllByPersonId(int personId)
        {
            ATSCEEntities context = new ATSCEEntities();
            IEnumerable<TimeSheetMaster> timeSheetMasters = from master in context.TimeSheetMasters
                                                       where master.PersonId == personId
                                                       select master;
            return timeSheetMasters;
        }

        public static IEnumerable<TimeSheetMaster> GetAllByAgentId(int agentId)
        {
            ATSCEEntities context = new ATSCEEntities();
            IEnumerable<TimeSheetMaster> timeSheetMasters = from master in context.TimeSheetMasters
                                                            where master.AgencyId == agentId
                                                            select master;
            return timeSheetMasters;
        }

        public static IEnumerable<TimeSheetMaster> GetAllBySupervisorId(int supervisorId)
        {
            ATSCEEntities context = new ATSCEEntities();
            IEnumerable<TimeSheetMaster> timeSheetMasters = from master in context.TimeSheetMasters
                                                            where master.ManagerId == supervisorId && master.Status == Convert.ToInt32(TimeSheetStatus.Submitted)
                                                            select master;
            return timeSheetMasters;
        }

        public string Save()
        {
            string result = string.Empty;
            try
            {
                using (var context = new ATSCEEntities())
                {
                    context.Entry(this).State = this.TimeSheetMasterId <= 0 ? System.Data.EntityState.Added : System.Data.EntityState.Modified;
                    context.Entry(this.Person).State = System.Data.EntityState.Unchanged;
                    context.Entry(this.Supervisor).State = System.Data.EntityState.Unchanged;
                    foreach (TimeSheetDetail detail in this.TimeSheetDetail.ToList())
                        context.Entry(detail).State = detail.TimeSheetDetailId <= 0 ? System.Data.EntityState.Added : System.Data.EntityState.Modified;

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Record is not saved");
            }

            return result;
        }

        public string SaveMasterOnly()
        {
            string result = string.Empty;
            try
            {
                using (var context = new ATSCEEntities())
                {
                    context.Entry(this).State = System.Data.EntityState.Modified;
                    context.Entry(this.Person).State = System.Data.EntityState.Unchanged;
                    context.Entry(this.Supervisor).State = System.Data.EntityState.Unchanged;
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Record is not saved");
            }

            return result;
        }

        public static bool Delete(int id)
        {
            try
            {
                ATSCEEntities context = new ATSCEEntities();
                TimeSheetMaster master = context.TimeSheetMasters.Find(id);
                context.TimeSheetMasters.Remove(master);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }


}
