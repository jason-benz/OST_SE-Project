using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaHub.Data.Model;

namespace MediaHub.Test.MediaSearchTest;

public class MockMediaApi : IMediaApi
{
    private readonly Movie MockMovie = MediaSearchTest.MockMovie.GetMovie();
    public async Task<IMovie> GetMovieById(int id)
    {
        if (id == 12)
        {
            return MockMovie;
        }
        throw new Exception("Movie not found");
    }

    public async Task<List<IMovie>> Search(string query)
    {
        var movies = new List<IMovie>();
        if (query == "MockMovie")
        {
            movies.Add(MockMovie);
            return movies;
        }

        if (query == DateTime.Now.Year.ToString())
        {
            movies.Add(new Movie(
                13,
                "MockMovie 2 " + DateTime.Now.Year.ToString(),
                "mockmovie.ch",
                new List<string>() { "Mock", "NotReal" },
                10, 
                "Mocked movie story", 
                "120", 
                "2022-04-04")
            );
            return movies;
        }
        throw new Exception("No Movies found");
    }
}