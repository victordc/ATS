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

        #region Setup Company

        /// <summary>
        /// Setup company
        /// </summary>
        /// <param name="setupCompany"></param>
        /// <returns></returns>
        public int SetupCompany(SetupCompany setupCompany)
        {
            int result = -1;
            try
            {
                //using (TransactionScope scope = new TransactionScope())
                {
                    //1. Insert company
                    setupCompany.Company.Save();

                    //2. Insert supervisors
                    foreach (RegisterModel item in setupCompany.Supervisors)
                    {
                        Supervisor supervisor = new Supervisor();
                        supervisor.PersonName = (string.IsNullOrEmpty(item.FullName)? item.UserName: item.FullName);
                        supervisor.UserName = item.UserName;
                        supervisor.Email = item.Email;
                        supervisor.Phone = "98765432"; //TODO: hardcoded 
                        supervisor.CompanyId = setupCompany.Company.CompanyId;

                        supervisor.Save();
                    }

                    //3. Insert Agents
                    foreach (RegisterModel item in setupCompany.Agents)
                    {
                        Agent agent = new Agent();
                        agent.PersonName = (string.IsNullOrEmpty(item.FullName) ? item.UserName : item.FullName);
                        agent.UserName = item.UserName;
                        agent.Email = item.Email;
                        agent.Phone = "98765432"; //TODO: hardcoded 
                        agent.Save();
                    }

                    //4. Insert Staffs
                    foreach (RegisterModel item in setupCompany.Staffs)
                    {
                        Staff staff = new Staff();
                        staff.PersonName = (string.IsNullOrEmpty(item.FullName) ? item.UserName : item.FullName);
                        staff.UserName = item.UserName;
                        staff.Email = item.Email;
                        staff.Phone = "98765432"; //TODO: hardcoded 
                        staff.SupervisorId = Person.GetByName(item.SupervisorName).PersonId;
                        staff.AgentId = Person.GetByName(item.AgentName).PersonId;
                        staff.Save();
                    }

                    //scope.Complete();
                }
            }
            //catch (TransactionAbortedException ex)
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion

        #region Membership

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserProfile> GetAllUsers()
        {
            return UserProfile.GetAllUsers();
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <returns></returns>
        public UserProfile GetUserById(int id)
        {
            return UserProfile.GetUserById(id);
        }

        /// <summary>
        /// Gets users by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<UserProfile> GetUsersByName(string name)
        {
            return UserProfile.GetUsersByName(name);
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<webpages_Roles> GetAllRoles()
        {
            return webpages_Roles.GetAllRoles();
        }

        /// <summary>
        /// Get role by name 
        /// </summary>
        /// <returns></returns>
        public webpages_Roles GetRoleByName(string name)
        {
            return webpages_Roles.GetRoleByName(name);
        }

        #endregion
    }
}
