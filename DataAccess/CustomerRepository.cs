using Car_Sharing.Data;
using Car_Sharing.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Sharing.DataAccess
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public override Customer? GetById(int id)
        {
            return _ef.Customers.Where(customer => customer.Id == id).Include(c=>c.Car).FirstOrDefault();
        }

        public Customer? GetByName(string name)
        {
            return _ef.Customers.Where(customer => customer.Name == name).SingleOrDefault();
        }

        public override IEnumerable<Customer> GetAll()
        {
            return _ef.Customers.Include(c=>c.Car);
        }

    }
}
