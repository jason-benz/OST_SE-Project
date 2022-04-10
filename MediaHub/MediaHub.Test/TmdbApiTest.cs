using System;
using MediaHub.Data.Model;
using Xunit;
namespace MediaHub.Test;

public class TmdbApiTest
{
    private readonly TmdbApi tmdb;
    
    public TmdbApiTest()
    {
        tmdb = new TmdbApi();
    }
    
    [Fact]
    public async void TestSearchEndpoint()
    {
        var res = await tmdb.Search("matrix");
        foreach (var movie in res)
        {
            Assert.NotNull(movie);
        }
    }

    [Fact]
    public async void TestMovieEndpoint()
    {
        var movie = await tmdb.GetMovieById(675353);
        Assert.NotNull(movie);
    }
}