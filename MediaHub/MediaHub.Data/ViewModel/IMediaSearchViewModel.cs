using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel;

public interface IMediaSearchViewModel
{
    public Task<Movie> GetMovieAsync(int id);

    public Task<List<Movie>> GetMoviesAsync(string searchString);
}