using Car_Sharing.Data;
using Car_Sharing.Repositories.Interface;
using Car_Sharing.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing
{
    internal class MenoOriginal
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
        public MenoOriginal()
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
    }
}
