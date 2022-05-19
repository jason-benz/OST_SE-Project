using MediaHub.Data.FeedModule.Model;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Data.ProfileModule.ViewModel
{
    public class UserProfileViewModel : IUserProfileViewModel
    {
        private readonly IUserProfileDataManager _userProfileDataManager;
        private readonly IFeedService _feedService;

        public UserProfileViewModel(IUserProfileDataManager userProfileDataManager, IFeedService feedService)
        {
            _userProfileDataManager = userProfileDataManager;
            _feedService = feedService;
        }

        public UserProfile? GetUserProfileById(string userId)
        {
            try
            {
                return _userProfileDataManager.GetUserProfileById(userId);
            }
            catch (Exception ex)
            {
                ILogService.Singleton.LogException("An unknown error occured while reading the userprofile from the database", ILogService.LogCategory.UserSuggestion, ex);
                return null;
            }
        }

        public UserProfile? GetUserProfileByUsername(string username)
        {
            try
            {
                return _userProfileDataManager.GetUserProfileByUsername(username);
            }
            catch (Exception e)
            {
                ILogService.Singleton.LogException("An unknown error occured while reading the userprofile from the database", ILogService.LogCategory.Identity, e);
                return null;
            }

        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            try
            {
                if (userProfile.Biography.Length > 255)
                {
                    userProfile.Biography = userProfile.Biography.Substring(0, 255);
                }
                _userProfileDataManager.UpdateUserProfile(userProfile);
                _feedService.AddToFeed(userProfile.UserId, Table.UserProfile);
            }
            catch (Exception e)
            {
                ILogService.Singleton.LogException("An unknown Error occured, while updating the user profile", ILogService.LogCategory.Identity, e);
                throw;
            }
        }

        public bool IsUsernameAvailable(string username)
        {
            return _userProfileDataManager.IsUsernameAvailable(username);
        }
    }
}
