using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Data.ProfileModule.ViewModel
{
    public interface IUserProfileViewModel
    {
        public UserProfile? GetUserProfileById(string userId);

        public UserProfile? GetUserProfileByUsername(string username);

        public void UpdateUserProfile(UserProfile userProfile);

        public bool IsUsernameAvailable(string username);
    }
}
