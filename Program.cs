using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Car_Sharing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    //neotvara swagger , zistit preco 
    app.UseSwagger();
    app.UseSwaggerUI();
    
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

Menu menu = new Menu();
menu.StartMenu();



















