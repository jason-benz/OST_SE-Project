using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel;

public interface IMediaService
{
    public Task<IMovie> GetMovie(int id);

    public Task<List<IMovie>> GetMoviesAsync(string searchString);
}