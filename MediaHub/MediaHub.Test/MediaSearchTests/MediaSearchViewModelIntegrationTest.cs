using System;
using System.Collections.Generic;
using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.MediaModule.Persistency;
using MediaHub.Data.MediaModule.ViewModel;
using MediaHub.Test.UserProfileTest;
using Xunit;

namespace MediaHub.Test.MediaSearchTests;

public class MediaSearchViewModelIntegrationTest
{
    private readonly IMediaSearchViewModel _mediaSearchViewModel;
    private readonly IMediaTableViewModel _mediaTableViewModel;
    public MediaSearchViewModelIntegrationTest()
    {
        _mediaSearchViewModel = new MediaSearchViewModel(new TmdbApi());
        _mediaTableViewModel = new MediaTableViewModel(new TmdbApi(), new UserProfileDataManagerMock());
    }

    [Fact]
    public void GetMovieById()
    {
        var movie = _mediaSearchViewModel.GetMovieAsync(299534).Result;
        var avengersMovie = MockMovie.GetAvengersEndgameMovie();
        Assert.True(avengersMovie.Equals(movie));
    }

        [Fact]
        public void GetMoviesByString()
        {
            List<IMediaTableViewModel.MovieAndRating> movies = _mediaTableViewModel.GetMoviesByNameAsync("avengers").Result;
            Assert.True(movies.TrueForAll(mar => mar.Movie.Title.Contains("avengers", StringComparison.OrdinalIgnoreCase)));
        }
    
        [Fact]
        public void GetMoviesByEmptyString()
        {
            List<IMediaTableViewModel.MovieAndRating> movies = _mediaTableViewModel.GetMoviesByNameAsync("").Result;
            Assert.True(movies.TrueForAll(mar => mar.Movie.Title.Contains(DateTime.Now.Year.ToString())));
        }
}