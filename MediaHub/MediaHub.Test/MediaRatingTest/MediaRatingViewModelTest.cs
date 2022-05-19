using System;
using System.Collections.Generic;
using System.Linq;
using MediaHub.Data.FeedModule.Model;
using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.MediaModule.ViewModel;
using MediaHub.Data.ProfileModule.Model;
using MediaHub.Test.FeedTest;
using MediaHub.Data.UserSuggestionModule.Model;
using MediaHub.Test.UserProfileTest;
using MediaHub.Test.UserSuggestionTest;
using Xunit;

namespace MediaHub.Test.MediaRatingTest;

public class MediaRatingVieModelTest
{
    private readonly UserProfileDataManagerMock _profileDataManager = new UserProfileDataManagerMock();
    private readonly IUserSuggestionEngine _userSuggestionEngine = new UserSuggestionEngineMock();
    private readonly IFeedService _feedService = new FeedServiceMock(new FeedDataManagerMock());
    private readonly IRatingViewModel _ratingViewModel;

    public MediaRatingVieModelTest()
    {
        _ratingViewModel = new RatingViewModel(_profileDataManager, _userSuggestionEngine, _feedService);
    }

    [Fact, Trait("Category", "Unit")]
    public void TestToggleIsAddedToProfileThrows()
    {
        Assert.Throws<InvalidOperationException>(_ratingViewModel.ToggleIsAddedToProfile);
    }

    [Fact, Trait("Category", "Unit")]
    public void TestSetRatingThrows()
    {
        Assert.Throws<InvalidOperationException>(() => _ratingViewModel.Rating = 0);
    }

    [Fact, Trait("Category", "Unit")]
    public void TestToggleIsAddedToProfileDoesNotThrowAfterLoad()
    {
        InjectTestRating();
        LoadViewModel();
        var exceptions = Record.Exception(() => _ratingViewModel.ToggleIsAddedToProfile());
        Assert.Null(exceptions);
    }
    [Fact, Trait("Category", "Unit")]
    public void TestSetRatingDoesNotThrowAfterLoad()
    {
        InjectTestRating();
        LoadViewModel();
        var exceptions = Record.Exception(() => _ratingViewModel.Rating = 1);
        Assert.Null(exceptions);
    }

    [Fact, Trait("Category", "Unit")]
    public void TestRatingContainsProfile()
    {
        InjectTestRating();
        LoadViewModel();
        _ratingViewModel.Rating = 3;
        Assert.Equal("MockId-1", _profileDataManager.UpdatedUserProfile.UserId);
    }

    [Fact, Trait("Category", "Unit")]
    public void TestRatingContainsMovie()
    {
        InjectTestRating(42);
        LoadViewModel();
        _ratingViewModel.Rating = 3;
        Assert.Equal(42, _profileDataManager.UpdatedUserProfile.Ratings.ElementAt(0).MovieId);
    }

    [Fact, Trait("Category", "Unit")]
    public void TestViewModelGeneratesNewRatingIfNone()
    {
        LoadViewModel();
        _ratingViewModel.Rating = 3;
        Assert.Equal("MockId-1", _profileDataManager.UpdatedUserProfile.Ratings.ElementAt(0).Profile.UserId);
    }

    private void InjectTestRating()
    {
        _profileDataManager.TestRating = new MediaRating();
    }

    private void InjectTestRating(int movieId)
    {
        InjectTestRating();
        _profileDataManager.TestRating.MovieId = movieId;
    }
    private void LoadViewModel()
    {
        _ratingViewModel.Load("MockId-1", new Movie(44)
        {
            Title = "",
            Genres = new List<string>(),
            Rating = 1,
            Runtime = "",
            ReleaseDate = "",
            Overview = "",
            PosterUrl = ""
        });
    }
}