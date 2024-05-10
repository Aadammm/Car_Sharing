using Car_Sharing.Models;
using Car_Sharing.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Car_Sharing.DataAccess
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public override Customer? GetById(int id)
        {
           return _ef.Customers.Include(c => c.Car).ThenInclude(c => c.CarCompany).SingleOrDefault(c=>c.Id==id);
            
        }


        [return: MaybeNull]
        public Customer GetByName(string name)
        {
            Customer? customer = _ef.Customers.Where(a => a.Name == name).SingleOrDefault();
            if (customer != null)
            {
                return customer;
            }
            return null;
        }


        public override IEnumerable<Customer> GetAll()
        {
            return _ef.Customers.Include(c=>c.Car).ThenInclude(c=>c.CarCompany);
        }

    }
}
