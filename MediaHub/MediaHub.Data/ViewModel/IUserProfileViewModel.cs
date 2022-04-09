using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel
{
    public interface IUserProfileViewModel
    {
        public UserProfile GetUserProfileById(string userId);

        public UserProfile GetUserProfileByUsername(string username);

        public void UpdateUserProfile(UserProfile userProfile);
    }
}
