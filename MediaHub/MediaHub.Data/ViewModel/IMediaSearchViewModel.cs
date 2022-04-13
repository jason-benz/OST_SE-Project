using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel;

public interface IMediaSearchViewModel
{
    public Task<IMovie> GetMovieAsync(int id);

    public Task<List<IMovie>> GetMoviesListAsync(string searchString);
}