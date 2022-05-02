using MediaHub.Data.MediaModule.Model;
using Newtonsoft.Json.Linq;

namespace MediaHub.Data.MediaModule.Helpers;

public class TmdbJsonParser
{
    private readonly string _basePosterPath;

    public TmdbJsonParser(string basePosterPath)
    {
        _basePosterPath = basePosterPath;
    }

    public List<Movie> ParseSearchEndpointJsonToMovieResults(JObject json, Dictionary<int, string> genres)
    {

        var movieResults = new List<Movie>();

        var results = json.Property("results")?.Value;
        if (results == null)
        {
            return new List<Movie>();
        }

        foreach (var movieJson in results)
        {
            Movie movie = ParseSearchJsonToMovie((JObject)movieJson, genres);
            movieResults.Add(movie);
        }

        return movieResults;
    }

    public Movie ParseMovieEndpointJsonToMovie(JObject json)
    {
        var genreJson = (JArray)json.Property("genres")?.Value;
        List<string> genres = ParseGenreIdsToString(genreJson);

        return new Movie(int.Parse(json.Property("id")?.Value.ToString()!))
        {
            Title = json.Property("title")?.Value.ToString(),
            PosterUrl = _basePosterPath + json.Property("poster_path")?.Value,
            Genres = genres,
            Rating = ParseRating(json.Property("vote_average")?.Value.ToString()),
            Overview = json.Property("overview")?.Value.ToString(),
            Runtime = json.Property("runtime")?.Value.ToString(),
            ReleaseDate = json.Property("release_date")?.Value.ToString()
        };
    }

    private static List<string> ParseGenreIdsToString(JArray? genreIds)
    {
        var genres = new List<string>();

        if (genreIds == null)
        {
            return genres;
        }

        foreach (var jToken in genreIds)
        {
            var genre = (JObject)jToken;
            genres.Add(genre.Property("name")?.Value!.ToString()!);
        }

        return genres;
    }

    private Movie ParseSearchJsonToMovie(JObject movieJson, Dictionary<int, string> genres)
    {
        var genresOfMovie = new List<string>();

        foreach (var key in (JArray)movieJson.Property("genre_ids")?.Value)
        {
            genresOfMovie.Add(genres[key.ToObject<int>()]);
        }

        return new Movie(int.Parse(movieJson.Property("id").Value.ToString()))
        {
            Title = movieJson.Property("original_title")?.Value.ToString(),
            PosterUrl = _basePosterPath + movieJson.Property("poster_path")?.Value,
            Genres = genresOfMovie,
            Rating = ParseRating(movieJson.Property("vote_average")?.Value.ToString()),
            Overview = movieJson.Property("overview")?.Value.ToString(),
            ReleaseDate = movieJson.Property("release_date")?.Value.ToString()
        };
    }

    public static JObject DeserializeResponse(string response)
    {
        var json = JObject.Parse(response);
        return json;
    }

    private static int ParseRating(string? json)
    {
        return (int)Math.Round(double.Parse(json));
    }

    public static Dictionary<int, string> ParseAllGenres(JObject json)
    {
        var genres = new Dictionary<int, string>();
        JArray? g = (JArray)json.Property("genres")?.Value;
        foreach (var jToken in g)
        {
            var genre = (JObject)jToken;
            genres.Add(genre.GetValue("id")!.ToObject<int>(), genre.GetValue("name")!.ToString());
        }

        return genres;
    }
}