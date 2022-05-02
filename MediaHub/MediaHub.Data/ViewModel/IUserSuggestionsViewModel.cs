using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel
{
    public interface IUserSuggestionsViewModel
    {
        IEnumerable<UserSuggestion> GetUserSuggestions(string userId, bool loadIgnoredSuggestions = false);

        void IgnoreUserSuggestion(UserSuggestion userSuggestion);
    }
}
