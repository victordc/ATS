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
        private readonly IObjectAccessRepository objectAccessRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AdminFacade()
        {
            adminRepository = new CodeTableRepository();
            objectAccessRepository = new ObjectAccessRepository();
        }

        #endregion   
    
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

        #region ObjectAccess

        /// <summary>
        /// Get all object accesses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ObjectAccess> GetAllObjectAccesses()
        {
            return objectAccessRepository.GetAll(); ;
        }

        /// <summary>
        /// Get object access by id
        /// </summary>
        /// <returns></returns>
        public ObjectAccess GetObjectAccessById(int objectAccessId)
        {
            return objectAccessRepository.GetSingle(r => r.ObjectAccessId == objectAccessId);
        }

        /// <summary>
        /// Get object accesses by role. E.g. Administrator/Staff/Supervisor
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IEnumerable<ObjectAccess> GetObjectAccessByRole(string role)
        {
            return objectAccessRepository.GetList(r => r.Role == role);
        }

        /// <summary>
        /// Get controllers by role.
        /// </summary>
        /// <returns></returns>
        public IList<ObjectAccess> GetControllers(string roleName)
        {
            return objectAccessRepository.GetList(r => r.RoleName == roleName);
        }

        /// <summary>
        /// Save an add/edit object access.
        /// </summary>
        /// <param name="oa"></param>
        /// <returns></returns>
        public string SaveObjectAccess(ObjectAccess oa)
        {
            switch (oa.EntityState)
            {

                case EntityState.Added:
                    objectAccessRepository.Add(oa);
                    break;
                case EntityState.Modified:
                    objectAccessRepository.Update(oa);
                    break;
            }
            return string.Empty;
        }

        /// <summary>
        /// Delete an object access.
        /// </summary>
        /// <param name="oa"></param>
        /// <returns></returns>
        public string DeleteObjectAccess(ObjectAccess oa)
        {
             objectAccessRepository.Remove(oa);
             return string.Empty;
        }

        #endregion
    }
}
