namespace MediaHub.Data.Model
{
    public interface IUserSuggestionDataManager
    {
        IEnumerable<UserSuggestion> GetSuggestedUsers(string userId);

        IEnumerable<int> GetLikedMovieIdsByUserId(string userId);

        IEnumerable<string> GetUserIdsToBeSuggested(IEnumerable<int> movieIds, IEnumerable<string> usersToIgnore);

        void AddUserSuggestion(UserSuggestion userSuggestion);
    }
}
