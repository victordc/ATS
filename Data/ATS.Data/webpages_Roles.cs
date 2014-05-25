using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    public partial class  webpages_Roles
    {
        #region Public Static Methods

        public static webpages_Roles GetById(int roleId)
        {
            using (var context = new ATSCEEntities())
            {
                return context.webpages_Roles.Single(r => r.RoleId == roleId);
            }
        }

        public static webpages_Roles GetRoleByName(string roleName)
        {
            using (var context = new ATSCEEntities())
            {
                return context.webpages_Roles.Single(r => r.RoleName == roleName);
            }
        }

        public static IEnumerable<webpages_Roles> GetRolesByName(string roleName)
        {
            using (var context = new ATSCEEntities())
            {
                return context.webpages_Roles.Where(r => r.RoleName.Contains(roleName));
            }
        }

        public static IEnumerable<webpages_Roles> GetAllRoles()
        {
            using (var context = new ATSCEEntities())
            {
                return context.webpages_Roles.ToList();
            }
        }

        #endregion
    }
}
