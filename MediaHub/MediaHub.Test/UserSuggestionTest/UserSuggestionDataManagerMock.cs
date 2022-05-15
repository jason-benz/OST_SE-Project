using MediaHub.Data.UserSuggestionModule.Model;
using System.Collections.Generic;

namespace MediaHub.Test.UserSuggestionTest
{
    public class UserSuggestionDataManagerMock : IUserSuggestionDataManager
    {
        public void AddUserSuggestion(UserSuggestion userSuggestion)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<int> GetLikedMovieIdsByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserSuggestion> GetSuggestedUsers(string userId, bool loadIgnoredSuggestions = true)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserSuggestion> GetSuggestedUsersLazyLoading(string userId, bool loadIgnoredSuggestions = true)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetUserIdsToBeSuggested(IEnumerable<int> movieIds, IEnumerable<string> usersToIgnore)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUserSuggestion(UserSuggestion userSuggestion)
        {
            throw new System.NotImplementedException();
        }
    }
}
