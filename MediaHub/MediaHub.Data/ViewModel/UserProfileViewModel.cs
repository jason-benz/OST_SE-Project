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

        public UserProfile? GetUserProfileById(string userId)
        {
            try
            {
                return _userProfileDataManager.GetUserProfileById(userId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UserProfile? GetUserProfileByUsername(string username)
        {
            try
            {
                return _userProfileDataManager.GetUserProfileByUsername(username);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            try
            {
                _userProfileDataManager.UpdateUserProfile(userProfile);
            }
            catch (Exception)
            {
                throw new Exception("An unknown error occured");
            }
        }

        public bool IsUsernameAvailable(string username)
        {
            return _userProfileDataManager.IsUsernameAvailable(username);
        }
    }
}
