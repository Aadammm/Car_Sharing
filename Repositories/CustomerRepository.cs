using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.Models;
using Car_Sharing.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Sharing.Repositories
{
    internal class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
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
        public Customer? GetCustomerWithCar(Customer customer)
        {
            return _ef.Customers.Include(customer => customer.Car).Where(c => c.Id == customer.Id).FirstOrDefault();
        }
        public void LoadSingleReference(Customer customer)
        {
            _ef.Entry(customer).Reference(c=>c.Car).Load();
          
        }
    }
}
