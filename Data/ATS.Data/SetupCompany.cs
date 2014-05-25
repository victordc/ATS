using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    public class SetupCompany
    {
        public Company Company { get; set; }
        public List<RegisterModel> RegisterModels { get; set; }
        public UserProfile Agent { get; set; }
    }
}
