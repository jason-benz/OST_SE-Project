using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel
{
    public class UserProfileViewModel : IUserProfileViewModel
    {
        private readonly IUserProfileDataManager _userProfileDataManager;

        public UserProfileViewModel(IUserProfileDataManager userProfileDataManager)
        {
            _userProfileDataManager = userProfileDataManager;
        }

        public UserProfile GetUserProfileById(string userId)
        {
            return _userProfileDataManager.GetUserProfileById(userId);
        }

        public UserProfile GetUserProfileByUsername(string username)
        {
            return _userProfileDataManager.GetUserProfileByUsername(username);
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            _userProfileDataManager.UpdateUserProfile(userProfile);
        }

        public bool IsUsernameAvailable(string username)
        {
            return _userProfileDataManager.IsUsernameAvailable(username);
        }
    }
}
