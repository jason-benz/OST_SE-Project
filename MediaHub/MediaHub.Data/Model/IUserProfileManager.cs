using MediaHub.Data.Persistency;

namespace MediaHub.Data.Model
{
    public interface IUserProfileManager
    {
        public UserProfile GetUserProfileById(string userId);

        public UserProfile GetUserProfileByUsername(string username);

        public Task<int> UpdateUserProfileAsync(UserProfile userProfile);
    }
}
