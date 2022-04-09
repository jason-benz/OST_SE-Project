using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel;

public class MediaService : IMediaService
{
    private readonly IMediaApi _api;
    public MediaService(IMediaApi api)
    {
        _api = api;
    }
    public async Task<List<IMovie>> GetMoviesAsync(string searchString)
    {
        if(searchString == String.Empty)
        {
            searchString = DateTime.Now.Year.ToString();
        }
        return await _api.Search(searchString);
    }

    public IMovie GetMovie(int id)
    {
        // get movie from service

        return null;
    }
}