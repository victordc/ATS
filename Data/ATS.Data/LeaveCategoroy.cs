using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    public partial class LeaveCategory
    {
        public static IEnumerable<LeaveCategory> getAll()
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.LeaveCategories;       
        }

        public static LeaveCategory getById(int id)
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.LeaveCategories.Where(c=>c.LeaveCategoryId == id).FirstOrDefault();
        }

    }
}
