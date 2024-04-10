using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Car_Sharing
{
    internal class Menu
    {
        ICompanyRepository repository = new CompanyRepository();
        public void StartMenu()
        {
            Console.WriteLine("1. Log in as a manager\r\n0. Exit");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    ManagerMenu();
                    break;
                case '0':
                    //exit aplication
                    break;
                default:
                    break;
            }
        }

        public void ManagerMenu()
        {
            Console.WriteLine("1. Company list\r\n2. Create a company\r\n0. Back");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    CompanyList();
                    break;
                case '2':
                    Console.Write("Write name of company: ");
                    string nameOfCompany = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nameOfCompany))
                    {
                        repository.AddEntity(new Company()
                        {
                            Name = nameOfCompany
                        });
                        if (repository.SaveChanges())
                        {
                            Console.WriteLine("The company was created!");
                        }
                        else
                            Console.WriteLine("Failed to create the company.");
                    }
                    else
                        Console.WriteLine("You must enter a name for the company.");

                    break;
                case '0':
                    StartMenu();
                    break;
                default:
                    break;
            }
        }

        public void CompanyList()
        {
            var listOfCompanies = repository.GetCompanies();
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
                    ManagerMenu();
            }
            else
            {
                Console.WriteLine("The company list is empty!");
                ManagerMenu();
            }
        }

        public void CompanyMenu(int indexOfCompany)
        {
            Company company = repository.GetSingleCompany(indexOfCompany);
            Console.WriteLine("{0} Company", company.Name);
            Console.WriteLine("1. Car list\r\n2. Create a car\r\n0. Back");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    CarsList(company.Id);
                    break;
                case '2':
                    repository.AddEntity(new Car()
                    {
                        Name = Console.ReadLine()
                    });
                    if (repository.SaveChanges())
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
        public void CarsList(int companyId)
        {
            var cars = repository.GetCars().Where(car => car.Company_Id == companyId);
            Console.WriteLine("Car list:");
            foreach (var car in cars)
            {
                Console.WriteLine("{0}. {1}",car.Id,car.Name);
            }
            ManagerMenu();
        }
    }
}
