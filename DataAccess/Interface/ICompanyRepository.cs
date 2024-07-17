using Car_Sharing.Models;

namespace Car_Sharing.DataAccess.Interface
{
    public interface ICompanyRepository
    {
         Company? GetByName(string name);

         void AddEntity(Company entity);

         IEnumerable<Company> GetAll();

         bool SaveChanges();












         Company? GetById(int id);
    }
}
