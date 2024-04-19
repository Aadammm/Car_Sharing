using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Car_Sharing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Car_Sharing.Repositories.Interface;
using Car_Sharing.Repositories;
using Car_Sharing.Models;
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001");

builder.Services.AddControllers();
var app = builder.Build();
//builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.UseRouting();
//app.UseEndpoints(endpoints =>
//{ 
//    endpoints.MapControllers(); 
//});
app.Run();

Menu menu = new Menu();
menu.StartMenu();