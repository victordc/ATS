using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.BusinessFacade
{
    public interface IAdminFacade
    {
        IList<CodeTable> GetAllCodeTables();
        CodeTable GetCodeTableById(int codeTableId);
        IList<CodeTable> GetCodeTableByGroup(string codeGroup);
        //void AddCodeTable(params CodeTable[] codeTables);
        //void UpdateCodeTable(params CodeTable[] codeTables);
        //void RemoveCodeTable(params CodeTable[] codeTables);

        //IList<ObjectAccess> GetObjectAccessesByRole(string roleName);
        //void AddObjectAccess(ObjectAccess employee);
        //void UpdateObjectAccess(ObjectAccess employee);
        //void RemoveObjectAccess(ObjectAccess employee);
    }
}
