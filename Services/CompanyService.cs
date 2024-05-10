using Car_Sharing.Data;
using Car_Sharing.Models;
using Car_Sharing.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.DataAccess;

namespace Car_Sharing.Services
{
    public class CompanyService
    {
        readonly ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }
        public Company GetByName(string name)
        {
            return companyRepository.GetByName(name);
        }
        public bool CreateAndSaveCompany( string name)
        {
            if (GetByName(name) == null)
            {
                companyRepository.AddEntity(new Company()
                {
                    Name = name
                });
                return SaveChanges();
            }
            return false;
        }
        public bool SaveChanges()
        {
            return companyRepository.SaveChanges();
        }
        public IEnumerable<Company> GetCompanies()
        {
            return companyRepository.GetAll();
        }
    }
}
