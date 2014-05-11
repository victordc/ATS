using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    public partial class TimeSheetMaster
    {
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
    }
}
