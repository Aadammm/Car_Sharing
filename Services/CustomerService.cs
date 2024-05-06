using Car_Sharing.Data;
using Car_Sharing.Models;
using Car_Sharing.Repositories;
using Car_Sharing.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Services
{
    public class CustomerService
    {
        readonly ICustomerRepository customerRepository;

        public CustomerService()
        {
            customerRepository = new CustomerRepository();
        }

        public bool CreateCustomer(string name)
        {
            if (customerRepository.GetByName(name) == null)
            {
                customerRepository.AddEntity(new Customer()
                {
                    Name = name
                });
                return customerRepository.SaveChanges();
            }
            return false;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return customerRepository.GetAll();
        }
        public bool SaveChange()
        {
            return customerRepository.SaveChanges();
        }
    }
}
