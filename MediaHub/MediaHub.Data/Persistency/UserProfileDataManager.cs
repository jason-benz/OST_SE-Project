using MediaHub.Data.Model;

namespace MediaHub.Data.Persistency
{
    public class UserProfileDataManager : IUserProfileDataManager
    {
        public UserProfile? GetUserProfileById(string userId)
        {
            using MediaHubDBContext context = new();
            return context.UserProfiles.SingleOrDefault(up => up.UserId == userId);
        }

        public UserProfile? GetUserProfileByUsername(string username)
        {
            using MediaHubDBContext context = new();
            return context.UserProfiles.SingleOrDefault(up => up.Username == username);
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            using MediaHubDBContext context = new();
            context.Update(userProfile);
            context.SaveChanges();
        }

        public bool IsUsernameAvailable(string username)
        {
            using MediaHubDBContext context = new();
            return !context.UserProfiles.Any(up => up.Username == username);
        }
    }
}
