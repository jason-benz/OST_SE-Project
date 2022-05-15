using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.UserSuggestionModule.Model;
using Microsoft.EntityFrameworkCore;

namespace MediaHub.Data.UserSuggestionModule.Persistency
{
    public class UserSuggestionDataManager : IUserSuggestionDataManager
    {
        private const byte MinimalRating = 7;
        private const int AmountOfRequiredMatches = 5;

        public IEnumerable<UserSuggestion> GetSuggestedUsersLazyLoading(string userId, bool loadIgnoredSuggestions = true)
        {
            using MediaHubDBContext context = new();
            return context.UserSuggestions
                .Where(s =>
                    s.IgnoreSuggestion == loadIgnoredSuggestions &&
                    (s.UserId1 == userId || s.UserId2 == userId))
                .ToList();
        }

        public IEnumerable<UserSuggestion> GetSuggestedUsers(string userId, bool loadIgnoredSuggestions = true)
        {
            using MediaHubDBContext context = new();
            return context.UserSuggestions
                .Include(s => s.UserProfile1)
                .Include(s => s.UserProfile2)
                .Where(s =>
                    s.IgnoreSuggestion == loadIgnoredSuggestions &&
                    (s.UserId1 == userId || s.UserId2 == userId))
                .ToList();
        }

        public IEnumerable<int> GetLikedMovieIdsByUserId(string userId)
        {
            using MediaHubDBContext context = new();
            return context.Ratings
                .Where(r => r.ProfileId == userId && (r.Rating >= MinimalRating || r.IsAddedToProfile))
                .Select(r => r.MovieId).ToList();
        }

        public IEnumerable<string> GetUserIdsToBeSuggested(IEnumerable<int> movieIds, IEnumerable<string> usersToIgnore)
        {
            using MediaHubDBContext context = new();

            return context.Ratings
                .Where(r =>
                    !usersToIgnore.Contains(r.ProfileId) &&
                    movieIds.Contains(r.MovieId) &&
                    (r.Rating >= MinimalRating || r.IsAddedToProfile))
                .GroupBy(r => r.ProfileId)
                .Where(group => group.Count() >= AmountOfRequiredMatches)
                .Select(group => group.Key)
                .ToList();
        }

        public void AddUserSuggestion(UserSuggestion userSuggestion)
        {
            using MediaHubDBContext context = new();
            context.Add(userSuggestion);
            context.SaveChanges();
        }

        public void UpdateUserSuggestion(UserSuggestion userSuggestion)
        {
            using MediaHubDBContext context = new();
            context.Update(userSuggestion);
            context.SaveChanges();
        }
    }
}
