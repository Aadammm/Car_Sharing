using Car_Sharing.Models;
using Car_Sharing.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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
        //public void LoadSingleReference(Customer customer)
        //{
        //        _ef.Entry(customer).Reference(c => c.Car).Load();
        //}
        public override Customer? GetById(int id)
        {
           return _ef.Customers.Include(c => c.Car).ThenInclude(c => c.CarCompany).SingleOrDefault(c=>c.Id==id);
            
        }
        public override IEnumerable<Customer> GetAll()
        {
            return _ef.Customers.Include(c=>c.Car).ThenInclude(c=>c.CarCompany);
        }
    }
}
