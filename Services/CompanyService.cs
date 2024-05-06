using Car_Sharing.Data;
using Car_Sharing.Models;
using Car_Sharing.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Services
{
    public class CompanyService
    {
        readonly ICompanyRepository companyRepository;

        public CompanyService()
        {
            companyRepository = new CompanyRepository();
         
        }

        public bool CreateCompany( string name)
        {
            if (companyRepository.GetByName(name) == null)
            {
                companyRepository.AddEntity(new Company()
                {
                    Name = name
                });
                return companyRepository.SaveChanges();
            }
            return false;
        }
        public IEnumerable<Company> GetCompanies()
        {
            return companyRepository.GetAll();
        }
    }
}
