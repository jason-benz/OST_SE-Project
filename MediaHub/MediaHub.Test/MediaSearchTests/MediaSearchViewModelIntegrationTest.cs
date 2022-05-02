using System;
using System.Collections.Generic;
using MediaHub.Data.Model;
using MediaHub.Data.ViewModel;
using Xunit;

namespace MediaHub.Test.MediaSearchTest;

public class MediaSearchViewModelIntegrationTest
{
    private readonly IMediaSearchViewModel _mediaSearchViewModel;

    public MediaSearchViewModelIntegrationTest()
    {
        _mediaSearchViewModel = new MediaSearchViewModel(new TmdbApi());
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
        List<Movie> movies = _mediaSearchViewModel.GetMoviesAsync("avengers").Result;
        Assert.True(movies.TrueForAll( movie => movie.Title.Contains("avengers", StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void GetMoviesByEmptyString()
    {
        List<Movie> movies = _mediaSearchViewModel.GetMoviesAsync("").Result;
        Assert.True(movies.TrueForAll(movie => movie.Title.Contains(DateTime.Now.Year.ToString())));
    }
}