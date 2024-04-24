
using Car_Sharing.Models;

namespace Car_Sharing.Repositories.Interface
{
    internal interface ICustomerRepository
    {
        public void AddEntity(Customer entity);

        public IEnumerable<Customer> GetAll();

        public bool SaveChanges();

        public Customer GetById(int id);

        public void RemoveEntity(Customer entity);
        public Customer GetByName(string name);
        public void LoadSingleReference(Customer customer);

    }
}
