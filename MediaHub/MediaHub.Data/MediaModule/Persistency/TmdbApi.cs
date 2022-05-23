using MediaHub.Data.MediaModule.Helpers;
using MediaHub.Data.MediaModule.Model;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace MediaHub.Data.MediaModule.Persistency;

public class TmdbApi : IMediaApi
{
    private readonly string _baseUrl;
    private readonly string _basePosterPath;
    private readonly string _apiKeyV3;
    private readonly TmdbJsonParser _jsonParser;
    private readonly Task<Dictionary<int, string>> _genreTask;

    public TmdbApi()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true).Build();
        _apiKeyV3 = configuration.GetConnectionString("TmdbApiKey") ?? throw new InvalidOperationException();
        _baseUrl = configuration.GetConnectionString("TmdbBaseUrl") ?? throw new InvalidOperationException();
        _basePosterPath = configuration.GetConnectionString("TmdbBasePosterPath") ?? throw new InvalidOperationException();

        _genreTask = GetAllGenres();
        _jsonParser = new TmdbJsonParser(_basePosterPath);
    }

    public async Task<List<Movie>> Search(string query)
    {
        var urlParams = new Dictionary<string, string>() { { "query", query } };
        var json = await GetResponseFromApi("/search/movie", urlParams);
        var genres = await _genreTask;
        return _jsonParser.ParseSearchEndpointJsonToMovieResults(json, genres);
    }

    public async Task<Movie> GetMovieById(int id)
    {
        var urlParams = new Dictionary<string, string>();
        var json = await GetResponseFromApi("/movie/" + id, urlParams);
        return _jsonParser.ParseMovieEndpointJsonToMovie(json);
    }

    private async Task<Dictionary<int, string>> GetAllGenres()
    {
        var json = await GetResponseFromApi("/genre/movie/list", new Dictionary<string, string>());
        return TmdbJsonParser.ParseAllGenres(json);
    }

    private async Task<JObject> GetResponseFromApi(string endpoint, Dictionary<string, string> urlParams)
    {
        var requestUrl = BuildRequestUri(endpoint, urlParams);
        var response = await ExecuteGetRequest(requestUrl);
        return TmdbJsonParser.DeserializeResponse(response);
    }

    private static async Task<string> ExecuteGetRequest(Uri requestUrl)
    {
        var client = new HttpClient();
        var response = client.GetAsync(requestUrl).Result;
        return await response.Content.ReadAsStringAsync();
    }

    private Uri BuildRequestUri(string endpoint, Dictionary<string, string> urlParams)
    {
        urlParams.Add("api_key", _apiKeyV3);
        urlParams.Add("language", "en-US");
        urlParams.Add("page", "1");
        urlParams.Add("include_adult", "false");
        return new Uri(QueryHelpers.AddQueryString(_baseUrl + endpoint, urlParams));
    }
}