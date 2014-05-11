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
    }
}
