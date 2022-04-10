using System.Web;
using MediaHub.Data.Helpers;
using MediaHub.Data.Model;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MediaHub.Data.Model;

public class TmdbApi : IMediaApi
{
    private static readonly string _baseUrl = "https://api.themoviedb.org/3";
    private static readonly string _basePosterPath = "https://image.tmdb.org/t/p/original";
    private static readonly string _apiKeyV3 = "8feb42ff0cda9ec9c0a5e015a846fdbd";
    private TmdbJsonParser jsonParser;
    private readonly Task<Dictionary<int, string>> _genreTask;

    // TODO apikey in a config file

    public TmdbApi()
    {
        _genreTask = GetAllGenres();
        jsonParser = new TmdbJsonParser(_basePosterPath);
    }

    public async Task<List<Movie>> Search(string query)
    {
        var urlParams = new Dictionary<string, string>() {{"query", query}};
        var json = await GetResponseFromApi("/search/movie", urlParams);
        var genres = await _genreTask;
        return await jsonParser.ParseSearchEndpointJsonToMovieResults(json, genres);
    }

    public async Task<Movie> GetMovieById(int id)
    {
        var urlParams = new Dictionary<string, string>();
        var json = await GetResponseFromApi("/movie/" + id, urlParams);
        var genreMap = await _genreTask;
        return jsonParser.ParseMovieEndpointJsonToMovie(json);
    }

    private static async Task<Dictionary<int, string>> GetAllGenres()
    {
        var json = await GetResponseFromApi("/genre/movie/list", new Dictionary<string, string>());

        var genres = new Dictionary<int, string>();
        JArray j = (JArray)json.Property("genres").Value;
        foreach (JObject genre in j)
        {
            genres.Add(genre.GetValue("id").ToObject<int>(), genre.GetValue("name").ToString());
            //genres.Add(genre.Property("id").ToObject<int>(), genre.Property("name").ToString());
        }

        return genres;
    }

    private static async Task<JObject> GetResponseFromApi(string endpoint, Dictionary<string, string> urlParams)
    {
        var requestUrl = BuildRequestUri(endpoint, urlParams);
        var response = await ExecuteGetRequest(requestUrl);
        return TmdbJsonParser.DeserializeResponse(response);
    }

    private static async Task<string> ExecuteGetRequest(Uri requestUrl)
    {
        var client = new HttpClient();
        var response = await client.GetAsync(requestUrl);
        return await response.Content.ReadAsStringAsync();
    }

    private static Uri BuildRequestUri(string endpoint, Dictionary<string, string>? urlParams)
    {
        urlParams = urlParams ?? new Dictionary<string, string>();
        urlParams.Add("api_key", _apiKeyV3);
        urlParams.Add("language", "en-US");
        urlParams.Add("page", "1");
        urlParams.Add("include_adult", "false");
        return new Uri(QueryHelpers.AddQueryString(_baseUrl + endpoint, urlParams));
    }
}