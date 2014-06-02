using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    [MetadataType(typeof(UserProfileData))]
    public partial class UserProfile
    {
        #region Properties

        public string RoleName { get; set; }
        public Person Person { get; set; }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<UserProfile> GetAllUsers()
        {
            using (var context = new ATSCEEntities())
            {
                return context.UserProfiles.ToList();
            }
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserProfile GetUserById(int id)
        {
            using (var context = new ATSCEEntities())
            {
                return context.UserProfiles.Single(r => r.UserId == id);
            }
        }

        /// <summary>
        /// Gets users by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IEnumerable<UserProfile> GetUsersByName(string name)
        {
            using (var context = new ATSCEEntities())
            {
                
                return context.UserProfiles.Where(r => r.UserName.Contains(name)).ToList();
            }
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserProfile GetUserByName(string userName)
        {
            using (var context = new ATSCEEntities())
            {
                return context.UserProfiles.Single(r => r.UserName == userName);
            }
        }



        #endregion
    }

    public class UserProfileData
    {
        //[Required(ErrorMessage = "User name is required")]
        //[DisplayName("User Name")]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "User name is required")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
    }
}
