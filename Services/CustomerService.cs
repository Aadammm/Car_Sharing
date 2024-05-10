using Car_Sharing.Data;
using Car_Sharing.Models;
using Car_Sharing.DataAccess;
using Car_Sharing.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;

namespace Car_Sharing.Services
{
    public class CustomerService
    {
        readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository cRepository)
        {
            customerRepository = cRepository;
        }
        public Customer GetByName(string name)
        {
            return customerRepository.GetByName(name);
        }
        public bool CreateAndSaveCustomer(string name)
        {
            Customer customer = GetByName(name);
            if (customer== null)
            {
                customerRepository.AddEntity(new Customer()
                {
                    Name = name
                });
                return SaveChange();
            }
            return false;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return customerRepository.GetAll();
        }

        public Car? AlreadyRentedCar(Customer customer)
        {
            if (customer.Rented_Car_Id != null)
            {
                Car? car = customer.Car;
                return customer.Car;
            }
            return null;
        }

        public bool RentCar(Customer customer, Car car)
        {
            customer.Car = car;
            car.CarCustomer = customer;
            return SaveChange();
        }

        public bool ReturnCar(Customer customer)
        {
            customer.Rented_Car_Id = null;
            return SaveChange();
        }

        public bool SaveChange()
        {
            return customerRepository.SaveChanges();
        }
    }
}
