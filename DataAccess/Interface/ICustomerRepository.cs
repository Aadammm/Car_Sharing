using Car_Sharing.Data;

namespace Car_Sharing.DataAccess.Interface
{
    public interface ICustomerRepository
    {
        public void AddEntity(Customer entity);

        public IEnumerable<Customer> GetAll();

        public bool SaveChanges();

        public Customer? GetById(int id);

        public Customer? GetByName(string name);
        public void Update(Customer entity);
        void Remove(Customer customer);
    }
}
