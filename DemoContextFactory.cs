using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace ServerlessSQLDemo
{
    public class DemoContextFactory : IDesignTimeDbContextFactory<DemoContext>
    {
        public DemoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DemoContext>();
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("SqlConnectionString"));

            return new DemoContext(optionsBuilder.Options);
        }
    }
}