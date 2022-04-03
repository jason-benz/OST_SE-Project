using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using MediaHub.Data;
using Newtonsoft.Json;

namespace MediaHub.Data.Model;

public class TmdbApi : IMediaApi
{
    private static readonly string _baseUrl = "https://api.themoviedb.org/3";
    private static readonly string _basePosterPath = "https://image.tmdb.org/t/p/original";
    private static readonly string _apiKeyV3 = "8feb42ff0cda9ec9c0a5e015a846fdbd";
    private static readonly string _apiAcessTokenV4 = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI4ZmViNDJmZjBjZGE5ZWM5YzBhNWUwMTVhODQ2ZmRiZCIsInN1YiI6IjYyNDU4ODQ4YmU1NWI3MDA1ZDhkMGQ5NiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.Er8U_sKaQ4lg6M-PWUitCag3ZD0j0P_Bvbh_8NAs_BQ";
    private Task<Dictionary<int, string>> genreTask;
    /*
     * Example request:
     * https://api.themoviedb.org/3/movie/550?api_key=8feb42ff0cda9ec9c0a5e015a846fdbd
     */

    // TODO database annotations
    // TODO integration tests --> marco 
    // TODO apikeys in a config file
    
    public TmdbApi()
    {
        genreTask = GetAllGenres();
    }

    private async static Task<string> GetResponseFromSubUrl(string requestUrl)
    {
            var client = new HttpClient();
            var result = "EMPTY";
            var response = await client.GetAsync(_baseUrl + requestUrl);
            result = await response.Content.ReadAsStringAsync();
            return result;
    }

    public async Task<List<IMovie>> Search(string query) // TODO: Add pagination support
    {
        query = HttpUtility.UrlEncode(query);
        var requestUrl = "/search/movie?api_key=" + _apiKeyV3 + "&language=en-US&query=" + query +
                            "&page=1&include_adult=false";
        var res = await GetResponseFromSubUrl(requestUrl);
        dynamic? json = JsonConvert.DeserializeObject(res);
        checkForNullThrowE(json, "JSON Response from TMDB API could not be deserialized. Response from api: " + res);
        
        var movieResults = new List<IMovie>();
        foreach (var mJson in json.results)
        {
            IMovie m = await ParseSearchJsonToMovie(mJson);
            movieResults.Add(m);
        }
        
        return movieResults;
    }

    public async Task<IMovie> GetMovieById(int id) //IMovieTMDB
    {
        var requestUrl = "/movie/" + id + "?api_key=" + _apiKeyV3 + "&language=en-US";
         var res = await GetResponseFromSubUrl(requestUrl);
         dynamic? json = JsonConvert.DeserializeObject(res);
         checkForNullThrowE(json, "JSON Response from TMDB API could not be deserialized. Response from api: " + res);

         var genres = ParseGenreIdsToString(json);
         Movie m = new Movie(
             int.Parse(json.id.ToString()), 
             json.title.ToString(), 
             _basePosterPath + json.poster_path.ToString(), 
             genres, 
             (int)Math.Round(double.Parse(json.vote_average.ToString())), 
             json.overview.ToString(), 
             json.runtime.ToString(), 
             json.release_date.ToString()); // TODO poster path needs to include whole url
         return m;
    }
    
    private async Task<Dictionary<int, string>> GetAllGenres()
    {
        var requestUrl = "/genre/movie/list?api_key=" + _apiKeyV3 + "&language=en-US";
        var res = await GetResponseFromSubUrl(requestUrl);
        dynamic? json = JsonConvert.DeserializeObject(res);
        checkForNullThrowE(json, "JSON Response from TMDB API could not be deserialized. Response from api: " + res);

        var genreDic = new Dictionary<int, string>();
        foreach (var genre in json.genres)
        {
            genreDic.Add(genre.id.ToObject<int>(), genre.name.ToString());
        }

        return genreDic;
    }

    // HELPER METHODS
    
    private async Task<IMovie> ParseSearchJsonToMovie(dynamic mJson)
    {
        var genres = await genreTask;
        var currGenres = new List<string>();
        if (mJson.genre_ids != null)
        {
            foreach (var currKey in mJson.genre_ids)
            {
                currGenres.Add(genres[currKey.ToObject<int>()]);
            }
        }

        return new Movie(
            int.Parse(mJson.id.ToString()),
            mJson.original_title.ToString(),
            _basePosterPath + mJson.poster_path.ToString(),
            currGenres, //done 
            (int) Math.Round(double.Parse(mJson.vote_average.ToString())),
            mJson.overview.ToString(),
            null,
            mJson.release_date.ToString()); 
    }
    
    private static List<string> ParseGenreIdsToString(dynamic? json)
    {
        var genres = new List<string>();
        if (json.genres != null)
        {
            foreach (var curr in json.genres)
            {
                genres.Add(curr.name.ToString());
            }
        }

        return genres;
    }

    private static void checkForNullThrowE(dynamic obj, string errMsg)
    {
        if (obj == null)
        {
            throw new Exception(errMsg);
        }
    }
}