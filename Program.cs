using Car_Sharing;
using Car_Sharing.Data;
using Car_Sharing.Models;
using Car_Sharing.Repositories;
using Car_Sharing.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.ComponentModel.Design;

ICarRepository carRepository = new CarRepository();

var cars = carRepository.GetAll();
foreach (var car in cars)
{
    Console.WriteLine(car.Name + " " + car.Company_Id);
}
ICustomerRepository customer = new CustomerRepository();
Customer cus = customer.GetById(1);
cus.Rented_Car_Id = null;
customer.SaveChanges();
if (cus.Car == null)
{
    Console.WriteLine("null");
}

if (cus.Rented_Car_Id == null)
{
    Console.WriteLine("null");
    Console.WriteLine(cus.Name + " " );
}
cus.Rented_Car_Id = 1;
customer.SaveChanges();
customer.LoadSingleReference(cus);
Console.WriteLine(cus.Name + " " + cus.Car.Name);




Menu menu = new Menu();
menu.StartMenu();

//IConfiguration configuration= new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
