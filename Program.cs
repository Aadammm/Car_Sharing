using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Car_Sharing.Presentation;

Console.WriteLine("Select application mode:");
Console.WriteLine("1: Console Application");
Console.WriteLine("2: API (Swagger / Postman)");
Console.Write("Your choice (1/2): ");
int mode;
while (!int.TryParse(Console.ReadLine(), out mode) || mode > 2 || mode < 0)
{
    Console.WriteLine("Please select the correct option.");
}

if (mode == 1)
{
    Menu menu = new();
    menu.StartMenu();
}
else if (mode == 2)
{
   Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("https://localhost:5001/swagger/index.html");
    Console.ResetColor();

    var builder = WebApplication.CreateBuilder(args);

    builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001");
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

    }
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

else
    Console.WriteLine("Thanks for using, bey bey");



















