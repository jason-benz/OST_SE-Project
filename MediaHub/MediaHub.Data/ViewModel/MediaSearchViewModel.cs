using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel;

public class MediaSearchViewModel : IMediaSearchViewModel
{
    private readonly IMediaApi _api;
    public MediaSearchViewModel(IMediaApi api)
    {
        _api = api;
    }
    public async Task<List<IMovie>> GetMoviesListAsync(string searchString)
    {
        if(searchString == String.Empty)
        {
            searchString = DateTime.Now.Year.ToString();
        }
        return await _api.Search(searchString);
    }

    public async Task<IMovie> GetMovieAsync(int id)
    {
        return await _api.GetMovieById(id);
    }
}