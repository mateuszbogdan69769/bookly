using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence;

namespace Infrastructure.Migrations
{
    public class BookingDbContextMigrationFactory : IDesignTimeDbContextFactory<BookingDbContext>
    {
        // Add new
        // dotnet ef migrations add <NAME OF MIGRATION> -c BookingDbContext --project Infrastructure --output-dir Migrations
        // dotnet ef database update -c BookingDbContext --project Infrastructure

        // Revert
        // dotnet ef database update <NAME OF MIGRATION> -c BookingDbContext --project Infrastructure

        // Remove last migration
        // dotnet ef migrations remove -c BookingDbContext --project Infrastructure

        public BookingDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("manualsettings.json", optional: true);
            IConfiguration configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<BookingDbContext>();
            var connectionString = configuration.GetConnectionString("Database");
            optionsBuilder.UseNpgsql(connectionString, options => { options.CommandTimeout(6000); });

            return new BookingDbContext(optionsBuilder.Options, configuration);
        }
    }
}