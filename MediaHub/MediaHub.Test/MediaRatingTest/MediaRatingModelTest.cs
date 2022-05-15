using System;
using System.Collections.Generic;
using System.Linq;
using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.MediaModule.ViewModel;
using MediaHub.Test.UserProfileTest;
using Xunit;

namespace MediaHub.Test.MediaRatingTest;

public class MediaRatingModelTest
{
    public MediaRatingModelTest()
    {
    }

    [Fact, Trait("Category", "Unit")]
    public void TestRatingExceedsMaximumThrows()
    {
        MediaRating rating = new MediaRating();
        var ex = Record.Exception(() => rating.Rating = 11);
        Assert.NotNull(ex);
    }
    [Fact, Trait("Category", "Unit")]
    public void TestRatingReturnsCorrectValue()
    {
        MediaRating rating = new MediaRating();
        rating.Rating = 10;
        Assert.Equal(10, rating.Rating);
    }
}