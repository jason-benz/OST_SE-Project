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

        public UserProfile GetUserProfileByUsername(string userName)
        {
            return _userProfileDataManager.GetUserProfileByUsername(userName);
        }

        public async Task<int> UpdateUserProfileAsync(UserProfile userProfile)
        {
            return await _userProfileDataManager.UpdateUserProfileAsync(userProfile);
        }
    }
}
