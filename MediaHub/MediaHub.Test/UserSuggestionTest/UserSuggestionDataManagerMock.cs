using MediaHub.Data.UserSuggestionModule.Model;
using System;
using System.Collections.Generic;

namespace MediaHub.Test.UserSuggestionTest
{
    public class UserSuggestionDataManagerMock : IUserSuggestionDataManager
    {
        public string UserId { get; set; }

        public void AddUserSuggestion(UserSuggestion userSuggestion)
        {
            UserId = userSuggestion.UserId1;
        }

        public IEnumerable<int> GetLikedMovieIdsByUserId(string userId)
        {
            return new List<int>() { 1 };
        }

        public IEnumerable<UserSuggestion> GetSuggestedUsers(string userId, bool loadIgnoredSuggestions = true)
        {
            switch (userId)
            {
                case "MockId-1":
                    return new List<UserSuggestion>();
                case "MockId-2":
                    return LoadMockUserSuggestions("MockId-2", loadIgnoredSuggestions);
                default:
                    throw new ArgumentOutOfRangeException(nameof(userId));
            }
        }

        public IEnumerable<UserSuggestion> GetSuggestedUsersLazyLoading(string userId, bool loadIgnoredSuggestions = true)
        {
            return new List<UserSuggestion>();
        }

        public IEnumerable<string> GetUserIdsToBeSuggested(IEnumerable<int> movieIds, IEnumerable<string> usersToIgnore)
        {
            List<string> userIds = new List<string>();

            foreach (int movieId in movieIds)
            {
                userIds.Add($"MockId-{movieId}");
            }

            return userIds;
        }

        public void UpdateUserSuggestion(UserSuggestion userSuggestion)
        {
            // No action required
        }

        private IEnumerable<UserSuggestion> LoadMockUserSuggestions(string userId, bool loadIgnoredSuggestions)
        {
            List<UserSuggestion> userSuggestions = new();

            userSuggestions.Add(new UserSuggestion
            {
                UserId1 = userId,
                IgnoreSuggestion = false
            });

            if (loadIgnoredSuggestions)
            {
                userSuggestions.Add(new UserSuggestion
                {
                    UserId1 = userId,
                    IgnoreSuggestion = true
                });
            }

            return userSuggestions;
        }
    }
}
