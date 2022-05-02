using MediaHub.Data.UserSuggestionModule.Model;

namespace MediaHub.Data.UserSuggestionModule.ViewModel
{
    public interface IUserSuggestionsViewModel
    {
        IEnumerable<UserSuggestion> GetUserSuggestions(string userId, bool loadIgnoredSuggestions = false);

        void IgnoreUserSuggestion(UserSuggestion userSuggestion);
    }
}
