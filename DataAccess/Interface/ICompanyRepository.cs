using Car_Sharing.Models;

namespace Car_Sharing.DataAccess.Interface
{
    public interface ICompanyRepository
    {
        public Company GetByName(string name);

        public void AddEntity(Company entity);

        public IEnumerable<Company> GetAll();

        public bool SaveChanges();

        public Company? GetById(int id);
    }
}
