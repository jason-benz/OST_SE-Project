using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.MediaModule.Persistency;
using MediaHub.Data.MediaModule.ViewModel;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using MediaHub.Data.ProfileModule.Persistency;
using MediaHub.Test.UserProfileTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

namespace MediaHub.Test.MediaTableViewModelTest;

public class MediaTableViewModelTest : IDisposable
{
    private readonly MediaTableViewModel _vm;
    private readonly UserProfile _profile;
    private readonly Movie _movie;

    private class MockMovieAPI : IMediaApi
    {
        public Task<Movie> GetMovieById(int id)
        {
            return Task<Movie>.Run(() => { return new Movie(id);});
        }

        public Task<List<Movie>> Search(string query)
        {
            throw new Exception("Movie not found");
        }
    }

    public MediaTableViewModelTest()
    {
        IMediaApi api = new MockMovieAPI(); 
        _movie = api.GetMovieById(1).Result;
        _profile = new UserProfile("MockId-1");
        var userProfileDataManager = new UserProfileDataManagerMock();
        userProfileDataManager.TestRating = new MediaRating()
        {
            MovieId = _movie.Id,
            Profile = _profile,
            Rating = 10,
            IsAddedToProfile = true
        };
        _vm = new MediaTableViewModel(api, userProfileDataManager);
        
    }

    [Fact, Trait("Category", "Unit")]
    public void TestMediaTableContainsMovie()
    {
        IMediaTableViewModel.MovieAndRating mar = _vm.GetMoviesOfUserProfileByUserIdAsync(_profile.UserId)
            .FirstOrDefault(new IMediaTableViewModel.MovieAndRating());
        Assert.Equal(_movie.Id, mar.Movie.Id);
    }
    
    [Fact, Trait("Category", "Unit")]
    public void TestMediaTableContainsRating()
    {
        IMediaTableViewModel.MovieAndRating mar = _vm.GetMoviesOfUserProfileByUserIdAsync(_profile.UserId)
            .FirstOrDefault(new IMediaTableViewModel.MovieAndRating());
        Assert.Equal(10, mar.UserRating);
    }

    [Fact, Trait("Category", "Unit")]
    public void TestGetMoviesByNameAsyncOfUnknownMovieCatchesException()
    {
        var list = _vm.GetMoviesByNameAsync("asdf").Result;
        Assert.Empty(list);
    }
    
    public void Dispose()
    {
    }
}