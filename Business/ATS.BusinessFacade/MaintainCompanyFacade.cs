using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Data.Model;
using ATS.Data.DAL;

namespace ATS.BusinessFacade
{
    public class MaintainCompanyFacade
    {
        private CompanyRepository repository;

        public MaintainCompanyFacade()
        {
            this.repository = new CompanyRepository();
        }

        public Company GetCompanyById(int companyId)
        {
            return repository.GetCompanyById(companyId);
        }
    }
}
