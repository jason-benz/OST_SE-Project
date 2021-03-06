using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.FeedModule.Model;
using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.ChatModule.Model;
using MediaHub.Data.ProfileModule.Model;
using MediaHub.Data.UserSuggestionModule.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MediaHub.Data.PersistencyLayer;

public class MediaHubDBContext : DbContext
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<MediaRating> Ratings { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<UserSuggestion> UserSuggestions { get; set; }
    public DbSet<FeedItem> FeedItems { get; set; }

    
    public DbSet<Contact> Contacts { get; set; }

    public DbSet<MediaComment> MediaComments { get; set; }

#pragma warning disable CS8618
    public MediaHubDBContext()
    {
    }

    public MediaHubDBContext(DbContextOptions options) : base(options)
    {
    }
#pragma warning restore CS8618
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DataConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>().HasOne(e => e.Receiver).WithMany().IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Message>().HasOne(e => e.Sender).WithMany().OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<UserSuggestion>().HasKey(s => new { s.UserId1, s.UserId2 });
        modelBuilder.Entity<UserSuggestion>().HasOne(s => s.UserProfile1).WithMany().OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<UserSuggestion>().HasOne(s => s.UserProfile2).WithMany().OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<FeedItem>().HasOne(s => s.UserProfile).WithMany().OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Contact>().HasOne(s => s.UserProfile).WithMany().OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Contact>().HasOne(s => s.ContactUserProfile).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}
