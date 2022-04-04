using MediaHub.Data.Persistency;

namespace MediaHub.Data.Model
{
    public class UserProfileManager : IUserProfileManager
    {
        public UserProfile GetUserProfileById(string userId)
        { 
            using MediaHubDBContext context = new();
            return context.UserProfiles.Single(up => up.UserId == userId);
        }

        public UserProfile GetUserProfileByUsername(string userName)
        {
            using MediaHubDBContext context = new();
            return context.UserProfiles.SingleOrDefault(up => up.Username == userName);
        }

        public async Task<int> UpdateUserProfileAsync(UserProfile userProfile)
        {
            await using MediaHubDBContext context = new();
            context.Update(userProfile);
            return await context.SaveChangesAsync();
        }
    }
}
