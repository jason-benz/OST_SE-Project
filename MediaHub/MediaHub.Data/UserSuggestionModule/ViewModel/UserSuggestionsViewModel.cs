using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.UserSuggestionModule.Model;

namespace MediaHub.Data.UserSuggestionModule.ViewModel
{
    public class UserSuggestionsViewModel : IUserSuggestionsViewModel
    {
        private readonly IUserSuggestionDataManager _userSuggestionDataManager;
        private readonly IContactDataManager _contactDataManager;

        public UserSuggestionsViewModel(IUserSuggestionDataManager userSuggestionDataManager, IContactDataManager contactDataManager)
        {
            _userSuggestionDataManager = userSuggestionDataManager;
            _contactDataManager = contactDataManager;
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

        public void AddToContact(string userId, string contactId)
        {
            _contactDataManager.AddContact(userId, contactId);
        }
    }
}
