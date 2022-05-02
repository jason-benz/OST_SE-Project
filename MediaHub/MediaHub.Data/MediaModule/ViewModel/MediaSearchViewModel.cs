using MediaHub.Data.MediaModule.Model;

namespace MediaHub.Data.MediaModule.ViewModel;

public class MediaSearchViewModel : IMediaSearchViewModel
{
    private readonly IMediaApi _api;
    public MediaSearchViewModel(IMediaApi api)
    {
        _api = api;
    }
    public async Task<List<Movie>> GetMoviesAsync(string searchString)
    {
        if (searchString == string.Empty)
        {
            searchString = DateTime.Now.Year.ToString();
        }

        try
        {
            return await _api.Search(searchString);
        }
        catch (Exception)
        {
            return new List<Movie>();
        }
    }

    public async Task<Movie> GetMovieAsync(int id)
    {
        return await _api.GetMovieById(id);
    }
}