﻿using ATS.Data.Model;
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

        /// <summary>
        /// Get all object accesses
        /// </summary>
        /// <returns></returns>
        IEnumerable<ObjectAccess> GetAllObjectAccesses();

        /// <summary>
        /// Get object access by id
        /// </summary>
        /// <returns></returns>
        ObjectAccess GetObjectAccessById(int objectAccessId);

        /// <summary>
        /// Get object accesses by role. E.g. Administrator/Staff/Supervisor
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IEnumerable<ObjectAccess> GetObjectAccessByRole(string role);

        /// <summary>
        /// Get controllers by role.
        /// </summary>
        /// <returns></returns>
        IList<ObjectAccess> GetControllers(string roleName);

        /// <summary>
        /// Save an add/edit object access.
        /// </summary>
        /// <param name="oa"></param>
        /// <returns></returns>
        string SaveObjectAccess(ObjectAccess oa);

        /// <summary>
        /// Delete an object access.
        /// </summary>
        /// <param name="oa"></param>
        /// <returns></returns>
        string DeleteObjectAccess(ObjectAccess oa);

                /// <summary>
        /// Setup company
        /// </summary>
        /// <param name="setupCompany"></param>
        /// <returns></returns>
        int SetupCompany(SetupCompany setupCompany);

        #region Membership

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserProfile> GetAllUsers();

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <returns></returns>
        UserProfile GetUserById(int id);

        /// <summary>
        /// Gets users by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerable<UserProfile> GetUsersByName(string name);

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns></returns>
        IEnumerable<webpages_Roles> GetAllRoles();

        /// <summary>
        /// Get role by name 
        /// </summary>
        /// <returns></returns>
        webpages_Roles GetRoleByName(string name);

        #endregion

    }
}
