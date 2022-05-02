using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaHub.Data.Model;

namespace MediaHub.Test.MediaSearchTest;

public class MockMediaApi : IMediaApi
{
    private readonly Movie MockMovie = MediaSearchTest.MockMovie.GetMovie();
    public async Task<Movie> GetMovieById(int id)
    {
        if (id == 12)
        {
            return MockMovie;
        }
        throw new Exception("Movie not found");
    }

    public async Task<List<Movie>> Search(string query)
    {
        var movies = new List<Movie>();
        if (query == "MockMovie")
        {
            movies.Add(MockMovie);
            return movies;
        }

        if (query == DateTime.Now.Year.ToString())
        {
            movies.Add(new Movie(13) { 
            Title = "MockMovie 2 " + DateTime.Now.Year.ToString(),
            PosterUrl = "mockmovie.ch",
            Genres = new List<string>() {"Mock", "NotReal"},
            Rating = 10,
            Overview = "Mocked movie story",
            Runtime = "120",
            ReleaseDate = "2022-04-04"
        }
        );
            return movies;
        }
        throw new Exception("No Movies found");
    }
}