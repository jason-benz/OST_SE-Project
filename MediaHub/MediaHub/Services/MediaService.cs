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
        movies = await _api.Search(searchString);
        return movies;
    }

    public IMovie GetMovie(int id)
    {
        // get movie from service

        return null;
    }
}