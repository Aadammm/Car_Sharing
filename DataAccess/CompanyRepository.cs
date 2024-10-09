using Car_Sharing.DataAccess;
using Car_Sharing.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Car_Sharing.Data
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public void ReloadCompany(Company company)
        {
            _ef.Entry(company).Collection(c => c.Cars).Query().Load();
        }
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


    }
}
