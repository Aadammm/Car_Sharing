﻿//using Car_Sharing.Data;
//using Car_Sharing.Models;
//using Car_Sharing.DataAccess;
//using Car_Sharing.DataAccess.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Runtime.ConstrainedExecution;

//namespace Car_Sharing.Services
//{
//    public class CustomerService(ICustomerRepository cRepository)
//    {
//        readonly ICustomerRepository customerRepository = cRepository;

//        public Customer? GetByName(string name)
//        {
//            var customer = customerRepository.GetByName(name);
//            if (customer is not null)
//            {
//                return customer;
//            }
//            return null;
//        }
//        public bool CreateAndSaveCustomer(string name)
//        {
//            Customer? customer = GetByName(name);
//            if (customer is null)
//            {
//                customerRepository.AddEntity(new Customer()
//                {
//                    Name = name
//                });
//                return SaveChange();
//            }
//            return false;
//        }

//        public IEnumerable<Customer> GetCustomers()
//        {
//            var customers = customerRepository.GetAll();
//            if (customers is not null)
//            {
//                return customers;
//            }
//            return [];
//        }

//        public Car? AlreadyRentedCar(Customer customer)
//        {
//            if (customer.Rented_Car_Id != null)
//            {
//                return customer.Car;
//            }
//            return null;
//        }

//        public bool RentCar(Customer customer, Car car)// kontrola ked je auto zarezervovane nemoze sa rezervovat znovu 
//        {
//            if (customer.Car is null)
//            {//chyba je v tom ze ef uz trackuje company v car, treba ju odpojit alebo zmenit logiku
//                //ale zas dalej sa vypisuje rented car company name, a takto je len id a nie company
//                //treba najst kde ef zacne sledovat company a odpojit ju ked
//                car.Company_Id = car.CompanyCar.Id;
//                car.CompanyCar = null;
//                customer.Car = car;
//                return SaveChange();
//            }
//            return false;
//        }

//        public bool ReturnCar(Customer customer)
//        {
//            customer.Rented_Car_Id = null;
//            return SaveChange();
//        }

//        public bool SaveChange()
//        {
//            return customerRepository.SaveChanges();
//        }
//    }
//}
