using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLySach.DAL.EF;
using System.IO;

namespace QuanLySach.DesignTimeDbContextFactory
{
    /// <summary>
    /// Design-time factory for EF Core migrations and tools.
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BookDbContext>
    {
        public BookDbContext CreateDbContext(string[] args)
        {
            // Build configuration from appsettings.json in the main project
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "QuanLySach"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("QuanLyCafeDatabase");
            var optionsBuilder = new DbContextOptionsBuilder<BookDbContext>();
            optionsBuilder.UseSqlServer(
                connectionString,
                sqlOptions => sqlOptions.MigrationsAssembly("QuanLySach.DAL.EF")
            );

            return new BookDbContext(optionsBuilder.Options);
        }
    }
}
