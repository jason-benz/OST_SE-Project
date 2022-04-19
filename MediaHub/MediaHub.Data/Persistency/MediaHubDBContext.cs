using MediaHub.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MediaHub.Data.Persistency;

public class MediaHubDBContext : DbContext
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<MediaRating> Ratings { get; set; }
    public MediaHubDBContext()
    {
    }

    public MediaHubDBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings_data.json", true, true)
                .AddJsonFile("appsettings.json",true,true)
                .Build();
            
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DataConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
