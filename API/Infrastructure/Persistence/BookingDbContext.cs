using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Domain.Enums;
using Npgsql;

namespace Infrastructure.Persistence;

public class BookingDbContext : DbContext, IBookingDbContext
{
    private readonly IConfiguration _configuration;

    public BookingDbContext(DbContextOptions<BookingDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), x => x.Namespace == "Infrastructure.Persistence.Configurations");
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(GetConnectionString());
        }
    }

    private string GetConnectionString()
    {
        var postGreSqlConnectionPattern = new NpgsqlConnectionStringBuilder()
        {
            Database = _configuration[ConfigConsts.DatabaseName],
            Host = _configuration[ConfigConsts.DatabaseHost],
            Port = int.Parse(_configuration[ConfigConsts.DatabasePort]),
            Username = _configuration[ConfigConsts.DatabaseLogin],
            Password = _configuration[ConfigConsts.DatabasePassword],
            ConnectionIdleLifetime = 60,
        };
        return postGreSqlConnectionPattern.ConnectionString;
    }
}
