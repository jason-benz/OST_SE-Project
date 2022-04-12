using MediaHub.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace MediaHub.Data.Persistency
{
    public class UserProfileDataManager : IUserProfileDataManager
    {
        public UserProfile GetUserProfileById(string userId)
        {
            using MediaHubDBContext context = new();
            var profile = context.UserProfiles.Include(p => p.Ratings).Single(up => up.UserId == userId);
            return profile;
        }

        public UserProfile GetUserProfileByUsername(string username)
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
    }
}
