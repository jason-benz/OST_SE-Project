using MediaHub.Data.MediaModule.Model;

namespace MediaHub.Data.MediaModule.ViewModel;

public interface IMediaSearchViewModel
{
    public Task<Movie> GetMovieAsync(int id);
}