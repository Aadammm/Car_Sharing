using Car_Sharing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;


namespace Car_Sharing.Data
{
    internal class EntityFramework : DbContext
    {

        public DbSet<Company> Companies { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }

        readonly IConfiguration config;

        public EntityFramework()
        {
            config = new ConfigurationBuilder()
           .AddJsonFile("Configuration/appsettings.json")
           .Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(config
                .GetConnectionString("DefaultConnection"),
                options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Project");
            modelBuilder.Entity<Company>().ToTable("Company").HasKey("Id");
            modelBuilder.Entity<Car>().ToTable("Car").HasKey("Id");
            modelBuilder.Entity<Customer>().ToTable("Customer").HasKey("Id");
            modelBuilder.Entity<Company>()
                 .HasMany(c => c.ListOfCompanyCars)
                 .WithOne(c => c.CarCompany)
                 .HasForeignKey(car => car.Company_Id);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.CarCompany)
                .WithMany(c => c.ListOfCompanyCars)
                .HasForeignKey(c => c.Company_Id);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Car)
                .WithOne(c => c.CarCustomer)
                .HasForeignKey<Customer>(c => c.Rented_Car_Id);
        }

    }
}
