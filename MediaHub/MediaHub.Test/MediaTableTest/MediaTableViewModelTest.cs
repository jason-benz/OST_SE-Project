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
    private MediaTableViewModel _vm;
    private UserProfile profile;
    private MediaRating rating;
    private IMediaApi api;
    private UserProfileDataManagerMock _userProfileDataManager;
    private Movie movie;

    private class MockMovieAPI : IMediaApi
    {
        public Task<Movie> GetMovieById(int id)
        {
            return Task<Movie>.Run(() => { return new Movie(id);});
        }

        public Task<List<Movie>> Search(string query)
        {
            throw new NotImplementedException();
        }
    }

    public MediaTableViewModelTest()
    {
        api = new MockMovieAPI(); 
        movie = api.GetMovieById(1).Result;
        profile = new UserProfile("MockId-1");
        _userProfileDataManager = new UserProfileDataManagerMock();
        _userProfileDataManager.TestRating = new MediaRating()
        {
            MovieId = movie.Id,
            Profile = profile,
            Rating = 10,
            IsAddedToProfile = true
        };
        _vm = new MediaTableViewModel(api, _userProfileDataManager);
        
    }

    [Fact, Trait("Category", "Unit")]
    public void TestMediaTableContainsMovie()
    {
        IMediaTableViewModel.MovieAndRating mar = _vm.GetMoviesOfUserProfileByUserIdAsync(profile.UserId)
            .FirstOrDefault(new IMediaTableViewModel.MovieAndRating());
        Assert.Equal(movie.Id, mar.Movie.Id);
    }
    
    [Fact, Trait("Category", "Unit")]
    public void TestMediaTableContainsRating()
    {
        IMediaTableViewModel.MovieAndRating mar = _vm.GetMoviesOfUserProfileByUserIdAsync(profile.UserId)
            .FirstOrDefault(new IMediaTableViewModel.MovieAndRating());
        Assert.Equal(10, mar.UserRating);
    }
    
    public void Dispose()
    {
    }
}