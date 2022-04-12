using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel;

public class RatingViewModel : IRatingViewModel
{
    private MediaRating? _rating;
    private UserProfile? _profile;
    private IMovie? _movie;
    private readonly IUserProfileDataManager _profileDataManager;
    private const string UndefinedProfileId = "0";
    private const int UndefinedMovieId = 0;
    public RatingViewModel(IUserProfileDataManager profileDataManager)
    {
        _profileDataManager = profileDataManager;
    }
    
    public void Load(string userId, IMovie movie)
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

    public void SetRating(byte value)
    {
        ExceptionIfNotLoadedFirst();
        _rating!.Rating = value;
        UpdateProfile();
    }
    
    private void LoadRatings()
    {
        if (_profile?.Ratings.Count == 0)
            MakeNewRatingForProfile();
        else
        {
            _rating = _profile?.Ratings
                           .Where(r => r.MovieId == _movie?.Id)
                           .FirstOrDefault(new MediaRating()); 
        }
           
    }

    private void MakeNewRatingForProfile()
    {
        _rating = new MediaRating()
        {
            Profile = _profile ?? new UserProfile(UndefinedProfileId),
            MovieId = _movie?.Id ?? UndefinedMovieId
        };
        _profile?.Ratings.Add(_rating);
    }

    private void UpdateProfile()
    {
        if (_profile != null)
        {
            _profileDataManager.UpdateUserProfile(_profile);
        }
    }

    private void ExceptionIfNotLoadedFirst()
    {
        if (_movie == null || _profile == null)
        {
            throw new InvalidOperationException("Call Load(), before using this method");
        }
    }
}