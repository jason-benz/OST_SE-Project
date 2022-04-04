using MediaHub.Data.Model;
using MediaHub.Data.Persistency;

namespace MediaHub.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileManager _userProfileManager = new UserProfileManager();

        public UserProfile GetUserProfileById(string userId)
        {
            return _userProfileManager.GetUserProfileById(userId);
        }

        public UserProfile GetUserProfileByUsername(string userName)
        {
            return _userProfileManager.GetUserProfileByUsername(userName);
        }

        public async Task<int> UpdateUserProfileAsync(UserProfile userProfile)
        {
            return await _userProfileManager.UpdateUserProfileAsync(userProfile);
        }
    }
}
