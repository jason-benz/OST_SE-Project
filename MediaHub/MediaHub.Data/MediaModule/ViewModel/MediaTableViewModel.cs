using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Data.MediaModule.ViewModel;

public class MediaTableViewModel : IMediaTableViewModel
{
    private readonly IMediaApi _api;
    private readonly IUserProfileDataManager _userProfileDataManager;

    public MediaTableViewModel(IMediaApi api, IUserProfileDataManager userProfileDataManager)
    {
        _api = api;
        _userProfileDataManager = userProfileDataManager;
    }
    public async Task<List<IMediaTableViewModel.MovieAndRating>> GetMoviesByNameAsync(string namePattern)
    {
        if (namePattern == string.Empty)
        {
            namePattern = DateTime.Now.Year.ToString();
        }

        try
        {
            var mediaList = await _api.Search(namePattern);
            List<IMediaTableViewModel.MovieAndRating> moviesAndRatings = new();
            foreach (var media in mediaList)
            {
                moviesAndRatings.Add(new IMediaTableViewModel.MovieAndRating()
                {
                    Movie = media,
                    UserRating = 0
                });
            }
            return moviesAndRatings;
        }
        catch (Exception)
        {
            return new List<IMediaTableViewModel.MovieAndRating>();
        }
    }

    public IEnumerable<IMediaTableViewModel.MovieAndRating> GetMoviesOfUserProfileByUserIdAsync(string userId)
    {
        var userProfile = _userProfileDataManager.GetUserProfileById(userId);
        var userRatings = userProfile!.Ratings
            .Where(r => r.IsAddedToProfile);

        foreach (var rating in userRatings)
        {
            yield return new IMediaTableViewModel.MovieAndRating()
            { Movie = _api.GetMovieById(rating.MovieId).Result, UserRating = rating.Rating };
        }
    }
}