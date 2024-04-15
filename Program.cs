using Car_Sharing;
using Car_Sharing.Data;
using Car_Sharing.Models;
using Car_Sharing.Repositories;
using Car_Sharing.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.ComponentModel.Design;



Menu menu = new Menu();
menu.StartMenu();

//IConfiguration configuration= new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
