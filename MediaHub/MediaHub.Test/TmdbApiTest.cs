using System;
using MediaHub.Data.Model;
using Xunit;
namespace MediaHub.Test;

public class TmdbApiTest
{
    private TmdbApi tmdb;
    
    public TmdbApiTest()
    {
        tmdb = new TmdbApi();
    }
    
    [Fact]
    public async void TestSearchEndpoint()
    {
        var res = await tmdb.Search("matrix");
    }

    [Fact]
    public async void TestMovieEndpoint()
    {
        var res = await tmdb.GetMovieById(675353);
    }
}