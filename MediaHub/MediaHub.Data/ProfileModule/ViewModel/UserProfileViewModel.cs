using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Data.ProfileModule.ViewModel
{
    public class UserProfileViewModel : IUserProfileViewModel
    {
        private readonly IUserProfileDataManager _userProfileDataManager;

        public UserProfileViewModel(IUserProfileDataManager userProfileDataManager)
        {
            _userProfileDataManager = userProfileDataManager;
        }

        public UserProfile? GetUserProfileById(string userId)
        {
            try
            {
                return _userProfileDataManager.GetUserProfileById(userId);
            }
            catch (Exception e)
            {
                ILogService.Singleton.LogException("An unknown error occured while reading the userprofile from the database", ILogService.LogCategory.Identity, e);
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
