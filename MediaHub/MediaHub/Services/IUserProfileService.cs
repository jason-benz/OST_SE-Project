using MediaHub.Data.Persistency;

namespace MediaHub.Services
{
    public interface IUserProfileService
    {
        public UserProfile GetUserProfileById(string userId);

        public UserProfile GetUserProfileByUsername(string userName);

        public Task<int> UpdateUserProfileAsync(UserProfile userProfile);
    }
}
