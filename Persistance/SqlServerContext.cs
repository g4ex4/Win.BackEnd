using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistance
{
    public class SqlServerContext : BaseDbContext
    {
        public SqlServerContext()
        {
        }
        public SqlServerContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("SqlServerDbContextConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'SqlServerDbContextConnection' not found.");

            optionsBuilder.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    SeedUsers(modelBuilder);
        //}

        //private void SeedUsers(ModelBuilder builder)
        //{
        //    if (Employees.Any())
        //        return;

        //    Employee admin = new Employee()
        //    {
        //        Id = 100,
        //        UserName = "Admin",
        //        Email = "1goldyshsergei1@gmail.com",
        //        EmailConfirmed = true,
        //        RoleId = 1,
        //        Education = "IT Academy",
        //        Experience = "10 Year",
        //        JobTitle = "Administrator",
        //    };


        //    PasswordHasher<Employee> passwordHasher = new PasswordHasher<Employee>();
        //    admin.PasswordHash = passwordHasher.HashPassword(admin, "111");
        //    builder.Entity<Employee>().HasData(admin);

        //}
    }
}
