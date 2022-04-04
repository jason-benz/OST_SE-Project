using MediaHub.Data.Model;

namespace MediaHub.Services;

public interface IMediaService
{
    public IMovie GetMovie(int id);

    public Task<List<IMovie>> GetMoviesAsync(string searchString);
}