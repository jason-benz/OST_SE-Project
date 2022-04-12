using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MediaHub.Data.Model;
using MediaHub.Data.Persistency;
using MediaHub.Data.ViewModel;
using MediaHub.Test.UserProfileTest;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Sdk;

namespace MediaHub.Test;

public class MediaRatingVieModelTest
{
    private readonly UserProfileDataManagerMock _profileDataManager = new UserProfileDataManagerMock();
    private readonly IRatingViewModel _ratingViewModel; 
    public MediaRatingVieModelTest()
    {
        _ratingViewModel = new RatingViewModel(_profileDataManager);
    }

    [Fact]
    public void TestToggleIsAddedToProfileThrows()
    {
        Assert.Throws<InvalidOperationException>(_ratingViewModel.ToggleIsAddedToProfile);
    }

    [Fact]
    public void TestSetRatingThrows()
    {
        Assert.Throws<InvalidOperationException>(() => _ratingViewModel.SetRating(0));
    }

    [Fact]
    public void TestToggleIsAddedToProfileDoesNotThrowAfterLoad()
    {
        InjectTestRating();
        LoadViewModel();
        var exceptions = Record.Exception(() => _ratingViewModel.ToggleIsAddedToProfile());
        Assert.Null(exceptions);
    }
    [Fact]
    public void TestSetRatingDoesNotThrowAfterLoad()
    {
        InjectTestRating();
        LoadViewModel();
        var exceptions = Record.Exception(() => _ratingViewModel.SetRating(1));
        Assert.Null(exceptions);
    }

    [Fact]
    public void TestRatingContainsProfile()
    {
        InjectTestRating();
        LoadViewModel();
        _ratingViewModel.SetRating(3);
        Assert.Equal("1234", _profileDataManager.UpdatedUserProfile.UserId);
    }

    [Fact]
    public void TestRatingContainsMovie()
    {
        InjectTestRating(42);
        LoadViewModel();
        _ratingViewModel.SetRating(3);
        Assert.Equal(42, _profileDataManager.UpdatedUserProfile.Ratings.ElementAt(0).MovieId);
    }

    [Fact]
    public void TestViewModelGeneratesNewRatingIfNone()
    {
        LoadViewModel();
        _ratingViewModel.SetRating(3);
        Assert.Equal("1234", _profileDataManager.UpdatedUserProfile.Ratings.ElementAt(0).Profile.UserId);
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
        _ratingViewModel.Load("1234", new Movie(44, "", "", new List<string>(), 1, "","",""));
    }
}