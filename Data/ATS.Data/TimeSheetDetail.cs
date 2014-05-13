using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    public partial class TimeSheetDetail
    {
        public static TimeSheetDetail GetById(int TimeSheetDetailId)
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.TimeSheetDetails.Find(TimeSheetDetailId);
        }

        public static IEnumerable<TimeSheetDetail> GetAll()
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.TimeSheetDetails;
        }

        public string Save()
        {
            string result = string.Empty;
            try
            {
                using (var context = new ATSCEEntities())
                {
                    if (this.TimeSheetDetailId <= 0)
                    {
                        context.TimeSheetDetails.Add(this);
                    }
                    else
                    {
                        context.Entry(this).State = System.Data.EntityState.Modified;
                    }
                    int rowAffected = context.SaveChanges();

                    if (rowAffected <= 0)
                        result = "Record is not saved";
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
                TimeSheetDetail detail = context.TimeSheetDetails.Find(id);
                context.TimeSheetDetails.Remove(detail);
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
