using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using Microsoft.EntityFrameworkCore;

namespace MediaHub.Data.ProfileModule.Persistency
{
    public class UserProfileDataManager : IUserProfileDataManager
    {
        public UserProfile? GetUserProfileById(string userId)
        {
            using MediaHubDBContext context = new();
            return context.UserProfiles.Include(p => p.Ratings).SingleOrDefault(up => up.UserId == userId);
        }

        public UserProfile? GetUserProfileByIdNoTracking(string userId)
        {
            using MediaHubDBContext context = new();
            return context.UserProfiles.AsNoTracking().SingleOrDefault(up => up.UserId == userId);
        }

        public List<UserProfile> GetAllUserProfiles()
        {
            using MediaHubDBContext context = new();
            return context.UserProfiles.ToList();
        }

        public UserProfile? GetUserProfileByUsername(string username)
        {
            using MediaHubDBContext context = new();
            return context.UserProfiles.Include(p => p.Ratings).SingleOrDefault(up => up.Username == username);
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
