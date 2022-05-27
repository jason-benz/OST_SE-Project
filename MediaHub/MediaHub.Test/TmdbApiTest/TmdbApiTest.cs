using MediaHub.Data.MediaModule.Persistency;
using Xunit;

namespace MediaHub.Test.TmdbApiTest;

public class TmdbApiTest
{
    private readonly TmdbApi tmdb;

    public TmdbApiTest()
    {
        tmdb = new TmdbApi();
    }

    /// <summary>
    /// Note to devs:
    /// Insert a breakpoint in the tests and check whether values seem to be correct, before pushing code.
    /// Cannot be done automatically as Search is non-deterministic.
    /// </summary>
    [Fact]
    public void TmdbSearch_Multiple()
    {
        var searchResult = tmdb.Search("matrix").Result;
        foreach (var movie in searchResult)
        {
            Assert.NotNull(movie);
        }
    }

    [Fact]
    public void TmdbSearch_Empty()
    {
        var searchResult = tmdb.Search(string.Empty).Result;
        Assert.Empty(searchResult);
    }

    [Fact]
    public void TmdbGetMovieById()
    {
        var movieId = 675353;
        var movie = tmdb.GetMovieById(movieId).Result;
        Assert.NotNull(movie);
        Assert.Equal(movieId, movie.Id);
    }

    [Fact]
    public void TmdbGetMovieById_NoResult()
    {
        var movie = tmdb.GetMovieById(-1).Result;
        Assert.Null(movie);
    }
}