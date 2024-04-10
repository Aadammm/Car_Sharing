﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Car_Sharing.Data
{
    internal class EntityFramework : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Car> Cars { get; set; }
        readonly IConfiguration config;

        public EntityFramework()
        {
            config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(config
                .GetConnectionString("DefaultConnection"),
                options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Project");
            modelBuilder.Entity<Company>().ToTable("Company").HasKey("Id");
            modelBuilder.Entity<Car>().ToTable("Car").HasKey("Id");



        }
    }
}
