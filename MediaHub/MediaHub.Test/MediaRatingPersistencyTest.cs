using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MediaHub.Data.Model;
using MediaHub.Data.Persistency;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace MediaHub.Test;

public class MediaRatingPersistencyTest : IDisposable
{
   private UserProfile profile;
   private MediaRating rating;
   
   public MediaRatingPersistencyTest()
   {
      using (MediaHubDBContext context = new())
      {
         profile = new UserProfile("test");
         rating = new MediaRating()
         {
            Profile = profile,
            ShowInProfile = false,
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
      Assert.Equal(this.rating.Id, profile.Ratings.ElementAt(0).Id);
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