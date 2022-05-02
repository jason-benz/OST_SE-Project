using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel;

public class MediaSearchViewModel : IMediaSearchViewModel
{
    private readonly IMediaApi _api;
    public MediaSearchViewModel(IMediaApi api)
    {
        _api = api;
    }
    public async Task<List<Movie>> GetMoviesAsync(string searchString)
    {
        if(searchString == String.Empty)
        {
            searchString = DateTime.Now.Year.ToString();
        }

        try
        {
            return await _api.Search(searchString);
        }
        catch (Exception) {
            return new List<Movie>();
        }
    }

    public async Task<Movie> GetMovieAsync(int id)
    {
        return await _api.GetMovieById(id);
    }
}