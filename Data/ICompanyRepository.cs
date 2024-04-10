using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Data
{
    internal interface ICompanyRepository
    {
        public bool SaveChanges();

        public IEnumerable<Company> GetCompanies();
        public void AddCompany<T>(T entityAdd);


    }
}
