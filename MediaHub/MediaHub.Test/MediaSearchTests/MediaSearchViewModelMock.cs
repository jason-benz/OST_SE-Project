using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        throw new Exception("Movie not found");
    }

    public async Task<List<IMovie>> GetMoviesAsync(string searchString)
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

        if (searchString == string.Empty)
        {
            List<IMovie> movies = new List<IMovie>();
            movies.Add(MockMovie);
            return movies;
        }
        throw new System.Exception("No Movies found");
    }
    
    
}