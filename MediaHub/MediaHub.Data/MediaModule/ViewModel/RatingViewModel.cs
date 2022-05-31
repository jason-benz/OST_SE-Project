using MediaHub.Data.FeedModule.Model;
using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.ProfileModule.Model;
using MediaHub.Data.UserSuggestionModule.Model;

namespace MediaHub.Data.MediaModule.ViewModel;

public class RatingViewModel : IRatingViewModel
{
    private MediaRating? _rating;
    private UserProfile? _profile;
    private Movie? _movie;
    private readonly IUserProfileDataManager _profileDataManager;
    private readonly IUserSuggestionEngine _userSuggestionEngine;
    private readonly IFeedService _feedService;

    public RatingViewModel(IUserProfileDataManager profileDataManager, IUserSuggestionEngine userSuggestionEngine, IFeedService feedService)
    {
        _profileDataManager = profileDataManager;
        _userSuggestionEngine = userSuggestionEngine;
        _feedService = feedService;
    }

    public void Load(string userId, Movie movie)
    {
        _movie = movie;
        _profile = _profileDataManager.GetUserProfileById(userId);
        LoadRatings();
    }

    public bool IsAddedToProfile
    {
        get => _rating!.IsAddedToProfile;
        private set => _rating!.IsAddedToProfile = value;
    }

    public void ToggleIsAddedToProfile()
    {
        ExceptionIfNotLoadedFirst();
        IsAddedToProfile = !IsAddedToProfile;
        UpdateProfile();
    }

    public byte Rating
    {
        get => _rating!.Rating;
        set
        {
            ExceptionIfNotLoadedFirst();
            _rating!.Rating = value;
            UpdateProfile();
        }
    }

    private void LoadRatings()
    {
        bool ratingsExist = _profile!.Ratings.Any(r => r.MovieId == _movie!.Id);
        if (!ratingsExist)
            MakeNewRatingForProfile();
        else
        {
            _rating = _profile.Ratings
                           .Where(r => r.MovieId == _movie!.Id)
                           .FirstOrDefault(new MediaRating());
        }

    }

    private void MakeNewRatingForProfile()
    {
        _rating = new MediaRating()
        {
            Profile = _profile!, 
            MovieId = _movie!.Id 
        };
        _profile!.Ratings.Add(_rating);
    }

    private void UpdateProfile()
    {
        _profileDataManager.UpdateUserProfile(_profile!);
        _userSuggestionEngine.StartUserSuggestionEngine(_profile!.UserId);
        _feedService.AddToFeed(_profile.UserId, Table.MediaRating, _movie!.Title);
    }

    private void ExceptionIfNotLoadedFirst()
    {
        if (_movie == null || _profile == null)
        {
            throw new InvalidOperationException("Call Load(), before using this method");
        }
    }
}