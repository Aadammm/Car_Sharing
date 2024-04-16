using Car_Sharing.Data;
using Car_Sharing.Repositories.Interface;
using Car_Sharing.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Runtime.ConstrainedExecution;

namespace Car_Sharing
{
    internal class Menu
    {
        const string emptyListMessageFormat = "The {0} list is empty!\n";
        const string entityCreatedMessageFormat = "The {0} was created!\n";
        const string entityCreationFailedMessageFormat = "Failed to add the {0}.\n";
        const string entityNameRequiredMessageFormat = "You must enter a name for the {0}.\n";
        const string promptForEntityNameFormat = "Write name of {0}: ";
        const string selectEntityPromptFormat = "Choose a {0}";
        readonly ICarRepository carRepository;
        readonly ICompanyRepository companyRepository;
        readonly ICustomerRepository customerRepository;
        public Menu()
        {
            carRepository = new CarRepository();
            companyRepository = new CompanyRepository();
            customerRepository = new CustomerRepository();
            
        }
        public int Choise(int numberOfChoise)
        {
            Console.Write(">>");
            int choise;
            while (int.TryParse(Console.ReadLine(), out choise) && choise < 0 || choise > numberOfChoise)
            {
                Console.WriteLine("Please choise correct number");
            }
            Console.WriteLine();
            return choise;

        }
        public void StartMenu()
        {
            Console.WriteLine("1. Log in as a manager\n2. Log in as a customer\n3. Create a customer\n0. Exit");
            switch (Choise(3))
            {
                case 1:
                    LogAsManager();
                    break;
                case 2:
                    CustomerMenu(LogAsCustomer());
                    break;
                case 3:
                    CreateCustomer();
                    StartMenu();
                    break;
                case 0:
                    break;
            }
        }
        private Customer LogAsCustomer()//null return 
        {
            var customers = customerRepository.GetAll().ToList();
            int listCount = customers.Count;
            if (listCount > 0)
            {
                Console.WriteLine(selectEntityPromptFormat, nameof(Customer));
                for (int i = 0; i < listCount; i++)
                {
                    Console.WriteLine($"{i + 1}. {customers[i].Name}");
                }
                Console.WriteLine("0. Back");
                int index = Choise(listCount);
                if (index == 0)
                    return null;

                customerRepository.LoadSingleReference(customers[index - 1]);
                return customers[index - 1];
            }
            Console.WriteLine(emptyListMessageFormat, nameof(Customer));
            return null;
        }
        private void RentCar(Customer customer)//uvidime ci bude fungovat update takymto sposobom
        {
            if (customer.Rented_Car_Id == null)
            {

                Car car = CarsList(CompanyList(), true);
                if (car != null)
                {
                    customer.Rented_Car_Id = car.Id;
                    if (customerRepository.SaveChanges())
                    {
                       customerRepository.LoadSingleReference(customer);
                        Console.WriteLine("You rented {0}\n", car.Name);
                    }
                    else
                    {
                        Console.WriteLine("Failed rent a car\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("You've already rented a car!\n");
            }

        }
        private void RentedCar(Customer customer)
        {
            if (customer.Rented_Car_Id != null)
            {
                Car? car = customer.Car;
                carRepository.LoadSingleReference(car);
                Console.WriteLine("Your rented car:\n{0}\nCompany:\n{1}\n", car.Name, car.Company.Name);
            }
            else
            {
                Console.WriteLine("You didn't rent a car!\n");
            }
        }
        private void ReturnRentedCar(Customer customer)
        {
            if (customer.Car != null)
            {
            customer.Rented_Car_Id = null;
            if (customerRepository.SaveChanges())
            {
                Console.WriteLine("You've returned a rented car!\n");
            }
            else
            {
                Console.WriteLine("Failed return a car\n");
            }
            }
            else
            Console.WriteLine("You didn't rent a car!\n");
        }
        private void CustomerMenu(Customer? customer)
        {
            if (customer != null)
            {
                Console.WriteLine("1. Rent a car\n2. Return a rented car\n3. My rented car\n0. Back");
                switch (Choise(3))
                {
                    case 1:
                        RentCar(customer);
                        CustomerMenu(customer);
                        break;
                    case 2:
                        ReturnRentedCar(customer);
                        CustomerMenu(customer);
                        break;
                    case 3:
                        RentedCar(customer);
                        CustomerMenu(customer);
                        break;
                    case 0:
                        StartMenu();
                        break;
                }
            }
            else
            {
                StartMenu();
            }
        }
        private void CreateCustomer()
        {
            Console.Write(promptForEntityNameFormat, nameof(Customer));
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
                    Console.WriteLine(entityCreatedMessageFormat, nameof(Customer));
                }
                else
                    Console.WriteLine(entityCreationFailedMessageFormat, nameof(Customer));
            }
            else
                Console.WriteLine(entityNameRequiredMessageFormat, nameof(Customer));
        }
        private void LogAsManager()
        {
            Console.WriteLine("1. Company list\n2. Create a company\n0. Back");
            switch (Choise(3))
            {
                case 1:
                    CompanyMenu(CompanyList());
                    break;
                case 2:
                    CreateCompany();
                    LogAsManager();
                    break;
                case 0:
                    StartMenu();
                    break;
                default:
                    break;
            }
        }

        private void CompanyMenu(Company company)
        {
            if (company != null)
            {
                Console.WriteLine("{0} Company", company.Name);
                Console.WriteLine("1. Car list\n2. Create a car\n0. Back");
                switch (Choise(3))
                {
                    case 1:
                        CarsList(company, false);
                        CompanyMenu(company);
                        break;
                    case 2:
                        CreateCar(company.Id);
                        CompanyMenu(company);
                        break;
                    case 0:
                        LogAsManager();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine(emptyListMessageFormat, nameof(company));
                LogAsManager();
            }
        }
        private void CreateCompany()
        {
            Console.Write(promptForEntityNameFormat, nameof(Company));
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
                Console.WriteLine(entityNameRequiredMessageFormat, nameof(Company));
        }
        private Car CarsList(Company company, bool choice)
        {
            var cars = company.Cars;
            int listCount = cars.Count;
            if (listCount > 0)
            {
                Console.WriteLine("Car list:");
                for (int i = 0; i < listCount; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, cars[i].Name);
                }
                if (choice)
                {
                    Console.WriteLine("0. Back");
                    int index = Choise(listCount);
                    if (index > 0)
                        return cars[index - 1];
                }
                return null;

            }
            else
                Console.WriteLine(emptyListMessageFormat, nameof(Car));
            return null;
        }
        private Company CompanyList()//return null 
        {
            var companies = companyRepository.GetAll().ToList();
            int listCount = companies.Count;
            if (listCount > 0)
            {

                Console.WriteLine(selectEntityPromptFormat, nameof(Company));
                for (int i = 0; i < listCount; i++)
                {
                    Console.WriteLine($"{i + 1}. {companies[i].Name}");
                }
                Console.WriteLine("0. Back");
                int index = Choise(listCount);
                if (index == 0)
                    return null;
                companyRepository.LoadAllReferences(companies[index - 1]);
                return  companies[index-1];
            }
            else
                Console.WriteLine(emptyListMessageFormat, nameof(Company));
            return null;

        }
        private void CreateCar(int companyId)
        {
            Console.WriteLine("Enter the car name:");
            string? name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
            {
                if (carRepository.GetByName(name)==null)
                {
                    carRepository.AddEntity(new Car()
                    {
                        Name = Console.ReadLine(),
                        Company_Id = companyId
                    });
                    if (carRepository.SaveChanges())
                    {
                        Console.WriteLine(entityCreatedMessageFormat, nameof(Car));
                    }
                    else
                        Console.WriteLine(entityCreationFailedMessageFormat, nameof(Car));
                }
                else
                    Console.WriteLine("Car already exists");
            }
        }

    }
}
