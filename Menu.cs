using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.Data;
using Car_Sharing.Models;
using Car_Sharing.Repositories;
using Car_Sharing.Repositories.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Car_Sharing
{
    internal class Menu
    {
        const string emptyListMessageFormat = "The {0} list is empty!";
        const string entityCreatedMessageFormat = "The {0} was created!";
        const string entityCreationFailedMessageFormat = "Failed to add the {0}.";
        const string entityNameRequiredMessageFormat = "You must enter a name for the {0}.";
        const string promptForEntityNameFormat = "Write name of {0}: ";
        const string selectEntityPromptFormat = "Choose a {0}";
        ICarRepository carRepository;
        ICompanyRepository companyRepository;
        ICustomerRepository customerRepository;
        public Menu()
        {
            carRepository = new CarRepository();
            companyRepository = new CompanyRepository();
            customerRepository = new CustomerRepository();
        }
        public void StartMenu()
        {
            Console.WriteLine("1. Log in as a manager\n2. Log in as a customer\n3. Create a customer\n0. Exit");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    LogAsManager();
                    break;
                case '2':
                    LogAsCustomer();
                    break;
                case '3':
                    CreateCustomer();
                    break;
                case '0':
                    //exit aplication
                    break;
                default:
                    break;
            }
        }
        public void LogAsManager()
        {
            Console.WriteLine("1. Company list\n2. Create a company\n0. Back");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    CompanyList();
                    break;
                case '2':
                    Console.Write(promptForEntityNameFormat,nameof(Company));
                    string? nameOfCompany = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nameOfCompany))
                    {
                        companyRepository.AddEntity(new Company()
                        {
                            Name = nameOfCompany
                        });
                        if (companyRepository.SaveChanges())
                        {
                            Console.WriteLine(entityCreatedMessageFormat, nameof(Company));
                        }
                        else
                            Console.WriteLine(entityCreationFailedMessageFormat, nameof(Company));
                    }
                    else
                        Console.WriteLine(entityNameRequiredMessageFormat,nameof(Company));

                    break;
                case '0':
                    StartMenu();
                    break;
                default:
                    break;
            }
        }
     
        public void CreateCustomer()
        {
            Console.Write("Write name of customer: ");
            string? nameOfCustomer = Console.ReadLine();
            if (!string.IsNullOrEmpty(nameOfCustomer))
            {
                customerRepository.AddEntity(new Customer()
                {
                    Name = nameOfCustomer,
                    Rented_Car_Id = null
                });
                if (customerRepository.SaveChanges())
                {
                    Console.WriteLine("The customer was created!");
                }
                else
                    Console.WriteLine("Failed to create the customer.");
            }
            else
                Console.WriteLine("You must enter a name for the customer.");
        }
        public void CompanyMenu(int companyId)
        {
            Company? company = companyRepository.GetById(companyId);
            Console.WriteLine("{0} Company", company.Name);
            Console.WriteLine("1. Car list\n2. Create a car\n0. Back");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    CarsList(company.Id);
                    break;
                case '2':
                    carRepository.AddEntity(new Car()
                    {
                        Name = Console.ReadLine()
                    });
                    if (companyRepository.SaveChanges())
                    {
                        Console.WriteLine("The Car was Added!");
                    }
                    else
                        Console.WriteLine("Failed to add the Car.");
                    break;
                case '0':
                    CompanyList();
                    break;
                default:
                    break;

            }
        }  

        //Tuple si skoncil vcera 
        //public void CustomerMenu(int customerId)
        //{
        //    Customer? customer = customerRepository.GetById(customerId);
        //    Console.WriteLine("{0} Customer", customer.Name);
        //    Console.WriteLine("1. Rent a car\n2. Return a rented car\n3. My rented car\n0. Back");
        //    switch (Console.ReadKey().KeyChar)
        //    {
        //        case '1':
        //            CarsList(customer.Id);
        //            break;
        //        case '2':
        //            carRepository.AddEntity(new Car()
        //            {
        //                Name = Console.ReadLine()
        //            });
        //            if (companyRepository.SaveChanges())
        //            {
        //                Console.WriteLine("The Car was Added!");
        //            }
        //            else
        //                Console.WriteLine("Failed to add the Car.");
        //            break;
        //        case '0':
        //            CompanyList();
        //            break;
        //        default:
        //            break;

        //    }
        //}   
        public void LogAsCustomer()
        {
            var customers = customerRepository.GetAll();
            if (customers.Count() > 0)
            {

                Console.WriteLine(selectEntityPromptFormat,nameof(Customer));
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0}. {1}", customer.Id, customer.Name);
                }
                int index = int.Parse(Console.ReadLine());
                if (index > 0)
                    CompanyMenu(index);

                else
                    LogAsManager();
            }
            else
            {
                Console.WriteLine(emptyListMessageFormat, nameof(Customer));
                LogAsManager();
            }
        }

         
        public void CompanyList()
        {
            var listOfCompanies = companyRepository.GetAll();
            if (listOfCompanies.Count() > 0)
            {

                Console.WriteLine("Choose a company");
                foreach (var company in listOfCompanies)
                {
                    Console.WriteLine($"{company.Id}. {company.Name}");
                }
                Console.WriteLine("0. Back");

                int index = int.Parse(Console.ReadLine());
                if (index > 0)
                    CompanyMenu(index);

                else
                    LogAsManager();
            }
            else
            {
                Console.WriteLine(emptyListMessageFormat, nameof(Company));
                LogAsManager();
            }
        }

        public void CarsList(int companyId)
        {
            var cars = carRepository.GetAll().Where(car => car.Company_Id == companyId);
            Console.WriteLine("Car list:");
            foreach (var car in cars)
            {
                Console.WriteLine("{0}. {1}", car.Id, car.Name);
            }
            LogAsManager();
        }


    }
}
