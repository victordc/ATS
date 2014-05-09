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
