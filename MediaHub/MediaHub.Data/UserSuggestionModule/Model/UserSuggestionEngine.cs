using MediaHub.Data.ContactsModule.Model;

namespace MediaHub.Data.UserSuggestionModule.Model
{
    public class UserSuggestionEngine : IUserSuggestionEngine
    {
        private readonly IUserSuggestionDataManager _userSuggestionDataManager;
        private readonly IContactDataManager _contactDataManager;

        public UserSuggestionEngine(IUserSuggestionDataManager userSuggestionDataManager, IContactDataManager contactDataManager)
        {
            _userSuggestionDataManager = userSuggestionDataManager;
            _contactDataManager = contactDataManager;
        }

        public Task StartUserSuggestionEngine(string userId)
        {
            return Task.Run(() =>
            {
                HashSet<string> userIdsToIgnore = new() { userId };
                userIdsToIgnore.UnionWith(GetSuggestedUserIds(userId));
                userIdsToIgnore.UnionWith(GetContacts(userId));

                var likedMovieIds = _userSuggestionDataManager.GetLikedMovieIdsByUserId(userId);
                var userIdsToBeSuggested = _userSuggestionDataManager.GetUserIdsToBeSuggested(likedMovieIds, userIdsToIgnore);

                foreach (var userIdToBeSuggested in userIdsToBeSuggested)
                {
                    AddUserSuggestion(userId, userIdToBeSuggested);
                }
            });
        }

        private IEnumerable<string> GetSuggestedUserIds(string userId)
        {
            var users = _userSuggestionDataManager.GetSuggestedUsersLazyLoading(userId);
            return users.Select(a => a.UserId1).Distinct().Union(users.Select(a => a.UserId2).Distinct());
        }

        private IEnumerable<string> GetContacts(string userId)
        {
            return _contactDataManager.GetContacts(userId);
        }

        private void AddUserSuggestion(string userId1, string userId2)
        {
            var userSuggestion = new UserSuggestion
            {
                UserId1 = userId1,
                UserId2 = userId2,
                IgnoreSuggestion = false
            };

            _userSuggestionDataManager.AddUserSuggestion(userSuggestion);
        }
    }
}
