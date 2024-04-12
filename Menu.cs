using Car_Sharing.Data;
using Car_Sharing.Models;
using Car_Sharing.Repositories;
using Car_Sharing.Repositories.Interface;

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
                   var companies= CompanyList();
                    ChooseCompany(companies);

                    break;
                case '2':
                    CreateCompany();
                    LogAsManager();
                    break;
                case '0':
                    StartMenu();
                    break;
                default:
                    break;
            }
        }
        public Company ChooseCompany(IEnumerable<Company> companies)
        {
            Console.WriteLine("Choose the company:");
            foreach (var company in companies)
            {
                Console.WriteLine($"{company.Id}. {company.Name}");
            }
            Console.WriteLine("0. Back");
            int index = int.Parse(Console.ReadLine());
            return companies.Where(company => company.Id == index).FirstOrDefault();
        }
        public void CompanyMenu(Company company)
        {
            Console.WriteLine("{0} Company", company.Name);
            Console.WriteLine("1. Car list\n2. Create a car\n0. Back");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    CarsList(company.Id);
                    CompanyMenu(company);
                    break;
                case '2':
                    CreateCar(company.Id);
                    CompanyMenu(company);
                    break;
                case '0':
                    LogAsManager();
                    break;
                default:
                    break;
            }
        }
        public IEnumerable<Company> CompanyList()
        {
         
                return companyRepository.GetAll();
        }
        public void ChooseCompany()
        {

        }
        public void CreateCompany()
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

        public void CreateCustomer()
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
        public void CreateCar(int companyId)
        {
            Console.WriteLine("Enter the car name:");
            carRepository.AddEntity(new Car()
            {
                Name = Console.ReadLine(),
                Company_Id = companyId
            });
            if (companyRepository.SaveChanges())
            {
                Console.WriteLine(entityCreatedMessageFormat, nameof(Car));
            }
            else
                Console.WriteLine(entityCreationFailedMessageFormat, nameof(Car));
        }


        public void LogAsCustomer()
        {
            var customers = customerRepository.GetAll();
            if (customers.Count() > 0)
            {

                Console.WriteLine(selectEntityPromptFormat, nameof(Customer));
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0}. {1}", customer.Id, customer.Name);
                }
                int index = int.Parse(Console.ReadLine());
                if (index > 0)
                    //nefunguje vyhladavat podla id 
                    //Customer customer = customerRepository.GetById(index);
                    CustomerMenu(customers.Where(id => id.Id == index).SingleOrDefault());

                else
                    LogAsManager();
            }
            else
            {
                Console.WriteLine(emptyListMessageFormat, nameof(Customer));
                LogAsManager();
            }
        }
        public void CustomerMenu(Customer customer)
        {
            Console.WriteLine("1. Rent a car\n2. Return a rented car\n3. My rented car\n0. Back");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    RentCar(customer);
                    break;
                case '2':
                    ReturnRentedCar(customer);
                    break;
                case '3':
                    RentedCar(customer);
                    CustomerMenu(customer);
                    break;
                case '0':
                    StartMenu();
                    break;
            }
        }
        //tu opravit aby company list zobrazoval iba company 
        public void RentCar(Customer customer)
        {
            CompanyList();

        }

        public void ReturnRentedCar(Customer customer)
        {

        }
        public void RentedCar(Customer customer)
        {

            if (customer.Rented_Car_Id > 0)
            {

            }
            Console.WriteLine("You didn't rent a car!\n");
          
        }
        public void CarsList(int companyId)
        {
            var cars = carRepository.GetAll().Where(car => car.Company_Id == companyId);
            if (cars.Count() > 0)
            {
                Console.WriteLine("Car list:");
                foreach (var car in cars)
                    Console.WriteLine("{0}. {1}", car.Id, car.Name);
            }
            Console.WriteLine(emptyListMessageFormat, nameof(Car));
        }




    }
}
