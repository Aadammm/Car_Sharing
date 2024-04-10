using Car_Sharing;
using Car_Sharing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


//IConfiguration configuration= new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

EntityFramework fr= new EntityFramework();
Company drg = new Company()
{
    Name="autvhhfghobus"
};
fr.Add(drg);
fr.SaveChanges();
var a = fr.Companies;
foreach (var b in a)
{
    Console.WriteLine(b.Name);
}
