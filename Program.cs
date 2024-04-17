using Car_Sharing;
using Car_Sharing.Data;
using Car_Sharing.Models;
using Car_Sharing.Repositories;
using Car_Sharing.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.ComponentModel.Design;

//Car car = new Car();
//ICarRepository repository = new CarRepository();
//ICompanyRepository companyRepository = new CompanyRepository();
//Company company = companyRepository.GetById(5);
//List<Car> cars = repository.GetCompanyCars(company);

//foreach (var c in cars)
//{
//    Console.WriteLine(c.Name+ " ***"+c.Id + "  ***"  + c.Company_Id + "  ***" + c.Name);
//}



Menu menu = new Menu();
menu.StartMenu();