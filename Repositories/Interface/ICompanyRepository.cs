using Car_Sharing.Models;

namespace Car_Sharing.Repositories.Interface
{
    internal interface ICompanyRepository
    {
        public void AddEntity(Company entity);

        public IEnumerable<Company> GetAll();

        public bool SaveChanges();

        public Company? GetById(int id);

        public void RemoveEntity(Company entity);
        public Company? GetCompanyWithCars(Company company);
        Company GetByName(string name);
        public void LoadAllReferences(Company company);
    }
}
