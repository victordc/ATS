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

        
    }
}
