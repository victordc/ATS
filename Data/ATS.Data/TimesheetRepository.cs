using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data
{
    public class TimesheetRepository
    {
        public static CodeTable GetCodeTableById(int codeTableId)
        {
            return CodeTable.GetById(codeTableId);
        }

        public static IEnumerable<CodeTable> GetCodeTables()
        {
            return CodeTable.GetAll();
        }


        public static LeavePlan GetLeavePlanById(int leavePlanId)
        {
            return LeavePlan.GetById(leavePlanId);
        }

        public static IEnumerable<LeavePlan> GetLeavePlans()
        {
            return LeavePlan.GetAll();
        }

        public static LeavePlan AddUpdateLeavePlan(LeavePlan leavePlan)
        {
            return LeavePlan.Save(leavePlan);
        }

        public static bool RemoveLeavePlan(int leavePlanId) 
        {
            return LeavePlan.Delete(leavePlanId);
        }

        public static IEnumerable<LeaveCategory> GetLeaveCategories()
        {
            return LeaveCategory.getAll();
        }

        public static Person GetPersonById(int PersonId)
        {
            return Person.GetById(PersonId);
        }

        public static IEnumerable<Person> GetAllPersons()
        {
            return Person.GetAll();
        }

        

    }
}
