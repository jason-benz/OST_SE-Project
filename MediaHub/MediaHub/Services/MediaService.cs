using MediaHub.Data.Model;

namespace MediaHub.Services;

public class MediaService : IMediaService
{
    private TmdbApi _api = new TmdbApi();
    public async Task<List<IMovie>> GetMoviesAsync(string searchString)
    {
        List<IMovie> movies = new List<IMovie>();
        // read data from other service 
        if(searchString == "")
        {
            searchString = DateTime.Now.Year.ToString();
        }
        return await _api.Search(searchString);
    }

    public async Task<IMovie> GetMovie(int id)
    {
        return await _api.GetMovieById(id);
    }
}