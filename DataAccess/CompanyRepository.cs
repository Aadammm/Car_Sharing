
using System.Diagnostics.CodeAnalysis;
using Car_Sharing.Models;
using Car_Sharing.DataAccess;
using Car_Sharing.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Sharing.Data
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public Company? GetByName(string name)
        {
            return _ef.Companies.Where(company => company.Name == name).SingleOrDefault();
        }

        public override IEnumerable<Company> GetAll()
        {
            return _ef.Companies.Include(c => c.Cars);
        }
        public override Company? GetById(int id)
        {
            return _ef.Companies.Where(c => c.Id == id).Include(c => c.Cars).SingleOrDefault();
        }

        public void LoadingCompanyReferences(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
