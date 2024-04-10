using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.Data;

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
                        repository.AddCompany(new Company()
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
            int index = 1;
            var a = repository.GetCompanies();
            if (a.Count() > 0)
            {

                Console.WriteLine("Choose a company");
                foreach (Company company in a)
                {
                    Console.WriteLine(index + " " + company.Name);
                    index++;
                }
            }
            else
            {
                Console.WriteLine("The company list is empty!");
            }
        }
    }
}
