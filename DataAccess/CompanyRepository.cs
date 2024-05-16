
using System.Diagnostics.CodeAnalysis;
using Car_Sharing.Models;
using Car_Sharing.DataAccess;
using Car_Sharing.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Sharing.Data
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository()
        {
        }

        public Company? GetByName(string name)
        {
            return _ef.Companies.Where(company => company.Name == name).SingleOrDefault();
        }


        public Company? GetCompanyWithCarReference(Company company)
        {
            return _ef.Companies.Include(company => company.ListOfCompanyCars)
                   .Where(car => car.Id == company.Id).FirstOrDefault();
        }


        public void LoadingCompanyReferences(Company company)
        {
            _ef.Entry(company).Collection(company => company.ListOfCompanyCars).Load();
        }


        public override Company? GetById(int id)
        {
            return _ef.Companies.Include(company => company.ListOfCompanyCars)
                   .SingleOrDefault(company => company.Id == id);
        }
    }
}
