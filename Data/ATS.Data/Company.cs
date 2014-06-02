using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    public partial class Company
    {
        public string Save()
        {
            string result = string.Empty;
            try
            {
                using (var context = new ATSCEEntities())
                {
                    if (this.CompanyId <= 0)
                    {
                        context.Companies.Add(this);
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
            catch (Exception)
            {
                // Log Exception here.
                throw new Exception("Record is not saved");
            }

            return result;
        }

        public static IEnumerable<Company> GetAll()
        {
            using (var context = new ATSCEEntities())
            {
                return context.Companies.ToList();
            }
        }
    }

    public class CompanyMetadata
    {
        [DisplayName("Company ID")]
        public int CompanyId { get; set; }

        [DisplayName("Company Description")]
        public string CompanyDescription { get; set; }

        [DisplayName("Company Address")]
        public string Address { get; set; }
    }
}
