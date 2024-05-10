
using System.Diagnostics.CodeAnalysis;
using Car_Sharing.Models;
using Car_Sharing.DataAccess;
using Car_Sharing.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Sharing.Data
{
    public class CompanyRepository :BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository()
        {
        }

        [return: MaybeNull]
        public Company GetByName(string name)
        {
            Company? company = _ef.Companies.Where(a => a.Name == name).SingleOrDefault();
            if (company != null)
            {
                return company;
            }
            return null;
        }


        public Company? GetCompanyWithCompanysCar(Company company)
        {
            return _ef.Companies.Include(c => c.ListOfCompanyCars).Where(c => c.Id == company.Id).FirstOrDefault();
        }


        public void LoadAllCompanyReference(Company company)
        {
            _ef.Entry(company).Collection(c => c.ListOfCompanyCars).Load();
        }


        public override Company? GetById(int id)
        {
            return _ef.Companies.Include(c => c.ListOfCompanyCars).SingleOrDefault(c => c.Id == id);
        }
    }
}
