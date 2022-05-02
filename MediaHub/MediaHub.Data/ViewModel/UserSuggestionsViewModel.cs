using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel
{
    public class UserSuggestionsViewModel : IUserSuggestionsViewModel
    {
        private readonly IUserSuggestionDataManager _userSuggestionDataManager;

        public UserSuggestionsViewModel(IUserSuggestionDataManager userSuggestionDataManager)
        {
            _userSuggestionDataManager = userSuggestionDataManager;
        }

        public IEnumerable<UserSuggestion> GetUserSuggestions(string userId, bool loadIgnoredSuggestions = false)
        {
            var suggestedUsers = _userSuggestionDataManager.GetSuggestedUsers(userId, loadIgnoredSuggestions);
            return suggestedUsers;
        }

        public void IgnoreUserSuggestion(UserSuggestion userSuggestion)
        {
            userSuggestion.IgnoreSuggestion = true;
            _userSuggestionDataManager.UpdateUserSuggestion(userSuggestion);
        }
    }
}
