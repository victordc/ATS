﻿using ATS.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Data;
using System.ComponentModel;

namespace ATS.Data.Model
{
    [MetadataType(typeof(ObjectAccessData))]
    public partial class ObjectAccess : IEntity
    {
        #region Properties

        public string RoleName
        {
            get
            {
                return Role;
            }
            set 
            {
                Role = value;
            }
        }

        public EntityState EntityState { get; set; }

        #endregion

        #region Public Methods

        public string Save()
        {
            string result = string.Empty;
            try
            {
                using (var context = new ATSCEEntities())
                {
                    if (this.ObjectAccessId <= 0)
                    {
                        context.ObjectAccesses.Add(this);
                    }
                    else
                    {
                        context.Entry(this).State = System.Data.EntityState.Modified;
                    }
                    int rowAffected = context.SaveChanges();

                    if (rowAffected <= 0)
                        result = "Record is not saved";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogError(ex.ToString());
                throw new Exception("Record is not saved");
            }

            return result;
        }

        public string Delete()
        {
            string result = string.Empty;
            try
            {
                using (var context = new ATSCEEntities())
                {
                    //context.ObjectAccesses.Remove(this);
                    context.Entry(this).State = System.Data.EntityState.Deleted;
                    int rowAffected = context.SaveChanges();
                    if (rowAffected <= 0)
                        result = "Record is not deleted";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogError(ex.ToString());
                throw new Exception("Record is not deleted");
            }

            return result;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Get all object accesses
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ObjectAccess> GetAllObjectAccesses()
        {
            IEnumerable<ObjectAccess> objectAccesses = null;
            using (var context = new ATSCEEntities())
            {
                objectAccesses = context.ObjectAccesses.ToList().OrderBy(r => r.ObjectAccessId);
            }

            return objectAccesses;
        }

        /// <summary>
        /// Get controllers by role.
        /// </summary>
        /// <returns></returns>
        public static List<ObjectAccess> GetControllers(string roleName)
        {
            List<ObjectAccess> objectAccesses = null;
            using (var context = new ATSCEEntities())
            {
                var query = (from r in context.ObjectAccesses
                            where r.Role == roleName
                            select new 
                            {
                                Controller = r.Controller,
                                Action = r.Action
                            }).ToList();

                objectAccesses = (from r in query
                                 select new ObjectAccess
                                 {
                                     Controller = r.Controller,
                                     Action = r.Action
                                 }).Distinct().OrderBy(r=>r.Controller).ToList();

            }

            return objectAccesses;
        }

        /// <summary>
        /// Get object access by id
        /// </summary>
        /// <returns></returns>
        public static ObjectAccess GetObjectAccessById(int objectAccessId)
        {
            ObjectAccess objectAccess = null;
            using (var context = new ATSCEEntities())
            {
                objectAccess = context.ObjectAccesses.Single(r => r.ObjectAccessId == objectAccessId);
            }

            return objectAccess;
        }

        /// <summary>
        /// Get object accesses by role. E.g. Administrator/Staff/Supervisor
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static IEnumerable<ObjectAccess> GetObjectAccessByRole(string role) {
            IEnumerable<ObjectAccess> objectAccesses = null;
            using (var context = new ATSCEEntities())
            {
                objectAccesses = context.ObjectAccesses.Where(p => p.Role == role).ToList();
            }

            return objectAccesses;
        }

        #endregion
    }

    public class ObjectAccessData
    {
        [Required(ErrorMessage = "Controller is required")]
        [DisplayName("Controller")]
        public string Controller { get; set; }

        [Required(ErrorMessage = "Action is required")]
        [DisplayName("Action")]
        public string Action { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [DisplayName("Role")]
        public string RoleName { get; set; }
    }
}
