using ATS.Data;
using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.BusinessFacade
{
    public class AdminFacade : IAdminFacade
    {

        private readonly ICodeTableRepository adminRepository;
        public AdminFacade() {
            adminRepository = new CodeTableRepository();
        }

        #region CodeTable

        /// <summary>
        /// Gets all code tables.
        /// </summary>
        /// <returns></returns>
        public IList<CodeTable> GetAllCodeTables()
        {
            return adminRepository.GetAll();
        }

        /// <summary>
        /// Gets code table by codeTableId.
        /// </summary>
        /// <param name="codeTableId"></param>
        /// <returns></returns>
        public CodeTable GetCodeTableById(int codeTableId)
        {
            return adminRepository.GetSingle(r => r.CodeTableId == codeTableId);
        }

        public IList<CodeTable> GetCodeTableByGroup(string codeGroup)
        {
            return adminRepository.GetList(r => r.GroupCode == codeGroup);
        }

        #endregion
        
    }
}
