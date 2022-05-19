using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.PersistencyLayer;
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
            try
            {
                return _userSuggestionDataManager.GetSuggestedUsers(userId, loadIgnoredSuggestions);
            } 
            catch(Exception ex)
            {
                ILogService.Singleton.LogException("An unknown error occured while loading user suggestions", ILogService.LogCategory.Identity, ex);
                return new List<UserSuggestion>();
            }
        }

        public void IgnoreUserSuggestion(UserSuggestion userSuggestion)
        {
            try
            {
                userSuggestion.IgnoreSuggestion = true;
                _userSuggestionDataManager.UpdateUserSuggestion(userSuggestion);
            }
            catch (Exception ex)
            {
                ILogService.Singleton.LogException("An unknown error occured while ignoring the user suggestion", ILogService.LogCategory.Identity, ex);
            }
        }

        public void AddToContact(string userId, string contactId)
        {
            try
            {
                _contactDataManager.AddContact(userId, contactId);
            }
            catch (Exception ex)
            {
                ILogService.Singleton.LogException("An unknown error occured while adding the user suggestion to the contacts", ILogService.LogCategory.Identity, ex);
            }
        }
    }
}
