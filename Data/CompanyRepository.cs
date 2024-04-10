using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Data
{
    internal class CompanyRepository:ICompanyRepository
    {
        EntityFramework ef = new EntityFramework();
        public bool SaveChanges()
        {
            return ef.SaveChanges() > 0;
        }

        public IEnumerable<Company> GetCompanies()
        {
            return ef.Companies;
        }

        public void AddCompany<T>(T entityAdd)
        {
            if(entityAdd!=null)
            ef.Add(entityAdd);
        }
        

    }
}
