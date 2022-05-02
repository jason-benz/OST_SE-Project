using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaHub.Data.Model;
using MediaHub.Data.ViewModel;

namespace MediaHub.Test.MediaSearchTest;

internal class MediaSearchViewModelMock : IMediaSearchViewModel
{
    private readonly Movie MockMovie = MediaSearchTest.MockMovie.GetMovie();
    public async Task<Movie> GetMovieAsync(int id)
    {
        if (id == 12)
        {
            return MockMovie;
        }
        throw new Exception("Movie not found");
    }

    public async Task<List<Movie>> GetMoviesAsync(string searchString)
    {
        if (searchString == "MockMovie")
        {
            List<Movie> movies = new List<Movie>()
            {
                MockMovie,
                new Movie(13)
                {
                    Title = "MockMovie 2",
                    PosterUrl = "mockmovie.ch",
                    Genres =  new List<string>() { "Mock", "NotReal" },
                    Rating = 10,
                    Overview = "Mocked movie story", 
                    Runtime = "120",
                    ReleaseDate =  "2022-04-04"
                }
            };
            return movies;
        }

        if (searchString == string.Empty)
        {
            List<Movie> movies = new List<Movie>();
            movies.Add(MockMovie);
            return movies;
        }
        throw new System.Exception("No Movies found");
    }
    
    
}