using Car_Sharing.Data;
using Car_Sharing.DataAccess.Interface;

namespace Car_Sharing.Services
{
    public class CompanyService
    {
        readonly ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }
        public Company? GetByName(string name)
        {
            return companyRepository.GetByName(name);
        }
        public bool CreateAndSaveCompany(string name)
        {
            if (GetByName(name) is null)
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
        public void Reload(Company company)
        {
            companyRepository.ReloadCompany(company);
        }

        public Company? GetById(int company_Id)
        {
        return  companyRepository.GetById(company_Id);
        }
    }
}