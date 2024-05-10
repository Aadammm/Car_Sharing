using Car_Sharing.Data;
using Car_Sharing.DataAccess.Interface;
using Car_Sharing.DataAccess;
using Car_Sharing.Models;
using System.Diagnostics.CodeAnalysis;
using Car_Sharing.Services;

namespace Car_Sharing.Presentation
{
    public class Menu
    {
        readonly CarService carService;
        readonly CompanyService companyService;
        readonly CustomerService customerService;

        public Menu()
        {
            carService = new CarService(new CarRepository());
            companyService = new CompanyService(new CompanyRepository());
            customerService = new CustomerService(new CustomerRepository());

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
                    CreateAndSaveCustomer();
                    StartMenu();
                    break;
                case 0:
                    break;
            }
        }

        private void LogAsManager()
        {
            Console.WriteLine("1. Company list\n2. Create a company\n"+Resource.back);
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
                Console.WriteLine("1. Car list\n2. Create a car\n"+Resource.back);
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
                Console.WriteLine(Resource.emptyListMessageFormat, nameof(company));
                LogAsManager();
            }
        }

        private void CustomerMenu(Customer? customer)
        {
            if (customer != null)
            {
                Console.WriteLine("1. Rent a car\n2. Return a rented car\n3. My rented car\n"+Resource.back);
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

        [return: MaybeNull]
        private Customer LogAsCustomer()
        {
            var customers = customerService.GetCustomers().ToList();
            int listCount = customers.Count;
            if (listCount > 0)
            {
                Console.WriteLine(Resource.selectEntityPromptFormat, nameof(Customer));
                Display(customers);
                Console.WriteLine(Resource.back);

                int index = Choise(listCount);
                if (index > 0)
                {
                    return customers[index - 1];
                }

                return null;
            }
            Console.WriteLine(Resource.emptyListMessageFormat, nameof(Customer));
            return null;
        }

        private void RentCar(Customer customer)
        {
            if (customer.Rented_Car_Id == null)
            {
                Car? car = CarsList(CompanyList(), true);
                if (car != null)
                {
                    bool carIsAlreadyRented = customerService.GetCustomers().Where(c => c.Rented_Car_Id == car.Id).FirstOrDefault() != null;

                    if (!carIsAlreadyRented && customerService.RentCar(customer, car))
                    {
                        Console.WriteLine("You rented {0}\n", car.Name);
                    }
                    else
                    {
                        Console.WriteLine("Failed rent a car\n");
                    }
                }
                else
                {
                    Console.WriteLine("This car is already Rented");
                }
            }
            else
            {
                Console.WriteLine("You've already rented a car!\n");
            }
        }

        private void RentedCar(Customer customer)
        {
            Car? car = customerService.AlreadyRentedCar(customer);

            if (car != null)
            {
                Console.WriteLine("Your rented car:\n{0}\nCompany:\n{1}\n", car.Name, car.CarCompany.Name);
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

                if (customerService.ReturnCar(customer))
                {
                    Console.WriteLine("You've returned a rented car!\n");
                }

                else
                {
                    Console.WriteLine("Failed return a car\n");
                }
            }
            else
            {
                Console.WriteLine("You didn't rent a car!\n");
            }
        }

        private void CreateAndSaveCustomer()
        {
            Console.Write(Resource.promptForEntityNameFormat, nameof(Customer));
            string? nameOfCustomer = Console.ReadLine();
            if (!string.IsNullOrEmpty(nameOfCustomer))
            {

                if (customerService.CreateAndSaveCustomer(nameOfCustomer))
                {
                    Console.WriteLine(Resource.entityCreatedMessageFormat, nameof(Customer));
                }

                else
                    Console.WriteLine(Resource.entityCreationFailedMessageFormat, nameof(Customer));
            }

            else
                Console.WriteLine(Resource.entityNameRequiredMessageFormat, nameof(Customer));
        }

        [return: MaybeNull]
        private Car CarsList(Company company, bool choice)
        {
            if (company == null)
                return null;

            var cars = carService.AllCarsWithCompany(company).ToList();

            int listCount = cars.Count;
            if (listCount > 0)
            {
                Console.WriteLine("Car list:");
                Display(cars);

                if (choice)
                {
                    Console.WriteLine(Resource.back);
                    int index = Choise(listCount);

                    if (index > 0)
                    {
                        return cars[index - 1];
                    }
                }
                return null;

            }
            else
                Console.WriteLine(Resource.emptyListMessageFormat, nameof(Car));
            return null;
        }

        private void CreateCar(int companyId)
        {
            Console.WriteLine(Resource.promptForEntityNameFormat, nameof(Car));
            string? name = Console.ReadLine();

            if (!string.IsNullOrEmpty(name))
            {

                if (carService.CreateAndSaveCar(companyId, name))
                {
                    Console.WriteLine(Resource.entityCreatedMessageFormat, nameof(Car));
                }

                else
                    Console.WriteLine(Resource.entityCreationFailedMessageFormat, nameof(Car));
            }
            else
                Console.WriteLine(Resource.entityNameRequiredMessageFormat, nameof(Car));
        }

        private void CreateCompany()
        {
            Console.Write(Resource.promptForEntityNameFormat, nameof(Company));
            string? nameOfCompany = Console.ReadLine();

            if (!string.IsNullOrEmpty(nameOfCompany))
            {

                if (companyService.CreateAndSaveCompany(nameOfCompany))
                {
                    Console.WriteLine(Resource.entityCreatedMessageFormat, nameof(Company));
                }

                else
                    Console.WriteLine(Resource.entityCreationFailedMessageFormat, nameof(Company));
            }

            else
                Console.WriteLine(Resource.entityNameRequiredMessageFormat, nameof(Company));
        }

        [return: MaybeNull]
        private Company CompanyList()//return null 
        {
            var companies = companyService.GetCompanies().ToList();
            int listCount = companies.Count;
            if (listCount > 0)
            {
                Console.WriteLine(Resource.selectEntityPromptFormat, nameof(Company));
                Display(companies);
                Console.WriteLine(Resource.back);

                int index = Choise(listCount);
                if (index > 0)
                {
                    return companies[index - 1];
                }
                return null;
            }

            else
                Console.WriteLine(Resource.emptyListMessageFormat, nameof(Company));
            return null;
        }

        private int Choise(int numberOfChoise)
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

        private void Display<T>(List<T> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                var item = entities[i];
                var propertyInfo = item.GetType().GetProperty("Name");
                if (propertyInfo != null)
                {
                    string name = (string)propertyInfo.GetValue(item);
                    Console.WriteLine($"{i + 1}. {name}");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. [No name]");
                }
            }
        }
    }

}

