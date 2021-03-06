using System;
using System.Linq;
using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MediaHub.Test.MediaRatingTest;

public class MediaRatingIntegrationTest : IDisposable
{
    private UserProfile profile;
    private MediaRating rating;

    public MediaRatingIntegrationTest()
    {
        using (MediaHubDBContext context = new())
        {
            profile = new UserProfile("test");
            rating = new MediaRating()
            {
                Profile = profile,
                IsAddedToProfile = false,
                Rating = 10
            };
            context.UserProfiles.Add(profile);
            context.Ratings.Add(rating);

            context.SaveChanges();
        }
    }

    [Fact]
    public void TestRatingIsPersisted()
    {
        using MediaHubDBContext context = new();
        var rating = context.Ratings.First(r => r.Id == this.rating.Id);
        Assert.Equal(this.rating.Id, rating.Id);
    }

    [Fact]
    public void TestRatingIsAccessibleThroughProfile()
    {
        using MediaHubDBContext context = new();
        var profile = context.UserProfiles.Include(p => p.Ratings).First(p => p.UserId == this.profile.UserId);
        Assert.Equal(rating.Id, profile.Ratings.ElementAt(0).Id);
    }
    
    [Fact]
    public void TestRatingContainsCorrectProfileId()
    {
        using MediaHubDBContext context = new();
        var profile = context.UserProfiles.Include(p => p.Ratings).First(p => p.UserId == this.profile.UserId);
        Assert.Equal(profile.UserId, rating.ProfileId);
    }
    public void Dispose()
    {
        using (MediaHubDBContext context = new())
        {
            context.Ratings.Remove(rating);
            context.UserProfiles.Remove(profile);
            context.SaveChanges();
        }
    }
}