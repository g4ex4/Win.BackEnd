﻿using Domain.Entities;
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

        
    }
}
