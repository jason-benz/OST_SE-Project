using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel
{
    public interface IUserProfileViewModel
    {
        public UserProfile GetUserProfileById(string userId);

        public UserProfile GetUserProfileByUsername(string userName);

        public Task<int> UpdateUserProfileAsync(UserProfile userProfile);
    }
}
