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

    /* Note to devs:
      Insert a breakpoint in the tests and check whether values seem to be correct, before pushing code.
      Cannot be done automatically as Search is non-deterministic. */
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