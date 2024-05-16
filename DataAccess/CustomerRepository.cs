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
           return _ef.Customers.Include(customer => customer.Car).ThenInclude(car => car.CompanyCar).SingleOrDefault(c=>c.Id==id);
        }

        public Customer? GetByName(string name)
        {
           return   _ef.Customers.Where(customer => customer.Name == name).SingleOrDefault();
        }

        public override IEnumerable<Customer> GetAll()
        {
            return _ef.Customers.Include(customer => customer.Car).ThenInclude(car => car.CompanyCar);
        }

    }
}
