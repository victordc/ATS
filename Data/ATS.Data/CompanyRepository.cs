using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Data.Model;

namespace ATS.Data.DAL
{
    public class CompanyRepository
    {
        private ATSCEEntities context;

        public CompanyRepository()
        {
            context = new ATSCEEntities();
        }

        public Company GetCompanyById(int companyId)
        {
            var query = from c in context.Companies
                        where c.CompanyId == companyId
                        select c;
            var company = query.FirstOrDefault<Company>();
            return company;
        }
    }
}
