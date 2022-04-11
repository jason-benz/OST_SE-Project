using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaHub;
using MediaHub.Data.Model;
using MediaHub.Data.ViewModel;

namespace MediaHub.Test.MediaSearchTest;

internal class MediaSearchViewModelMock : IMediaSearchViewModel
{
    private readonly Movie MockMovie = MediaSearchTest.MockMovie.GetMovie();
    public async Task<IMovie> GetMovieAsync(int id)
    {
        if (id == 12)
        {
            return MockMovie;
        }
        throw new System.Exception("Movie not found");
    }

    public async Task<List<IMovie>> GetMoviesListAsync(string searchString)
    {
        if (searchString == "MockMovie")
        {
            List<IMovie> movies = new List<IMovie>()
            {
                MockMovie,
                new Movie(13,
                    "MockMovie 2",
                    "mockmovie.ch",
                    new List<string>() { "Mock", "NotReal" },
                    10, 
                    "Mocked movie story", 
                    "120", 
                    "2022-04-04")
            };
            return movies;
        }

        if (searchString == String.Empty)
        {
            List<IMovie> movies = new List<IMovie>();
            movies.Add(MockMovie);
            return movies;
        }
        throw new System.Exception("No Movies found");
    }
    
    
}