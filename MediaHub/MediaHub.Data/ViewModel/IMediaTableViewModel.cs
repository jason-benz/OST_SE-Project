using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel;

public interface IMediaTableViewModel
{
    public class MovieAndRating
    {
        public Movie Movie { get; set; }
        public int UserRating { get; set; }
    }
    public Task<List<MovieAndRating>> GetMoviesByNameAsync(string namePattern);
    public IEnumerable<MovieAndRating> GetMoviesOfUserProfileByUserIdAsync(string userId);
}