using MediaHub.Data.Model;

namespace MediaHub.Data.Persistency
{
    public class UserProfileDataManager : IUserProfileDataManager
    {
        public UserProfile GetUserProfileById(string userId)
        {
            using MediaHubDBContext context = new();
            return context.UserProfiles.Single(up => up.UserId == userId);
        }

        public UserProfile GetUserProfileByUsername(string username)
        {
            using MediaHubDBContext context = new();
            return context.UserProfiles.SingleOrDefault(up => up.Username == username);
        }

        public async Task<int> UpdateUserProfileAsync(UserProfile userProfile)
        {
            await using MediaHubDBContext context = new();
            context.Update(userProfile);
            return await context.SaveChangesAsync();
        }
    }
}
