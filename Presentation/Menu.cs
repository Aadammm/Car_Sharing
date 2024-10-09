using Car_Sharing.Data;
using Car_Sharing.DataAccess;

using Car_Sharing.Services;
using System.Reflection;
using Car_Sharing.Properties;

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
            Console.WriteLine("1. Company list\n2. Create a company\n" + Resource.back);
            switch (Choise(3))
            {
                case 1:
                    CompanyMenu(ChooseCompany());
                    break;
                case 2:
                    CreateCompany();
                    LogAsManager();
                    break;
                case 0:
                    StartMenu();
                    break;
            }
        }

        private void CompanyMenu(Company? company)
        {
            if (company is not null)
            {
                Console.WriteLine("{0} Company", company.Name);
                Console.WriteLine("1. Car list\n2. Create a car\n" + Resource.back);
                switch (Choise(3))
                {
                    case 1:
                        ChooseCar(company, false);
                        CompanyMenu(company);
                        break;
                    case 2:
                        CreateCar(company.Id);
                        companyService.Reload(company);
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
            if (customer is not null)
            {
                Console.WriteLine("1. Rent a car\n2. Return a rented car\n3. My rented car\n" + Resource.back);
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
        private Customer? LogAsCustomer()
        {
            List<Customer> customers = customerService.GetCustomers().ToList();
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
            if (!   customer.Rented_Car_Id.HasValue)
            {
                Car? car = ChooseCar(ChooseCompany(), true);
                if (car is not null)
                {

                    if (customerService.RentCar(customer, car))
                    {
                        car.Customer_Id = customer.Id;
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
                Console.WriteLine("You've already rented a car!");
            }
        }

        private void RentedCar(Customer customer)
        {

            if (customer.Rented_Car_Id.HasValue)
            {
                Car? car = carService.GetById(customer.Rented_Car_Id.Value);
                Console.WriteLine("Your rented car:\n{0}\nCompany:\n{1}\n", car.Name, car.Company.Name);
            }
            else
            {
                Console.WriteLine("You didn't rent a car!\n");
            }
        }

        private void ReturnRentedCar(Customer customer)
        {
            if (customer.Rented_Car_Id.HasValue)
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

        private Car? ChooseCar(Company? company, bool choice)
        {
            if (company == null)
                return null;

            var cars = company.Cars.Where(car => !car.Customer_Id.HasValue).ToList();
            var carsCount = cars.Count;
            if (carsCount > 0)
            {
                Console.WriteLine("Car list:");
                Display(cars.ToList());

                if (choice)
                {
                    Console.WriteLine(Resource.back);
                    int index = Choise(carsCount);

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

        private Company? ChooseCompany()
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
                    var company = companies[index - 1];
                    return company;
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

        private void Display<T>(List<T>? entities)
        {
            if (entities is not null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    T? item = entities[i];
                    if (item is not null)
                    {
                        PropertyInfo? propertyInfo = item.GetType().GetProperty("Name");

                        if (propertyInfo is not null)
                        {
                            var name = propertyInfo.GetValue(item);
                            Console.WriteLine($"{i + 1}. {name?.ToString()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. [No name]");
                    }
                }
            }
        }
    }

}

