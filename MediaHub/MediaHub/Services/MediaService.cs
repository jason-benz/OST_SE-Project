using MediaHub.Data.Model;

namespace MediaHub.Services;

public class MediaService : IMediaService
{
    private TmdbApi _api = new TmdbApi();
    public async Task<List<IMovie>> GetMoviesAsync()
    {
        List<IMovie> movies = new List<IMovie>();
        // read data from other service 
        movies = await _api.Search("Spiderman");
        return movies;
    }

    public IMovie GetMovie(int id)
    {
        // get movie from service

        return null;
    }
}