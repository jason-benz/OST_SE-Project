namespace MediaHub.Data.UserSuggestionModule.Model
{
    public interface IUserSuggestionDataManager
    {
        IEnumerable<UserSuggestion> GetSuggestedUsersLazyLoading(string userId, bool loadIgnoredSuggestions = true);

        IEnumerable<UserSuggestion> GetSuggestedUsers(string userId, bool loadIgnoredSuggestions = true);

        IEnumerable<int> GetLikedMovieIdsByUserId(string userId);

        IEnumerable<string> GetUserIdsToBeSuggested(IEnumerable<int> movieIds, IEnumerable<string> usersToIgnore);

        void AddUserSuggestion(UserSuggestion userSuggestion);

        void UpdateUserSuggestion(UserSuggestion userSuggestion);
    }
}
