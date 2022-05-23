using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.UserSuggestionModule.Model;
using MediaHub.Data.UserSuggestionModule.ViewModel;
using MediaHub.Test.ContactTest;
using MediaHub.Test.LogTests;
using System.Linq;
using Xunit;

namespace MediaHub.Test.UserSuggestionTest
{
    public class UserSuggestionsViewModelTest
    {
        private readonly IUserSuggestionsViewModel _userSuggestionsViewModel;

        public UserSuggestionsViewModelTest()
        {
            _userSuggestionsViewModel = new UserSuggestionsViewModel(new UserSuggestionDataManagerMock(), new ContactDataManagerMock());
            LogService.Singleton = new LogServiceMock();
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("MockId-1", false, 0)]
        [InlineData("MockId-2", false, 1)]
        [InlineData("MockId-2", true, 2)]
        [InlineData("MockId-3", true, 0)]
        [InlineData("MockId-3", false, 0)]
        public void GetUserSuggestions(string userId, bool loadIgnoredSuggestions, int expectedResult)
        {
            var suggestedUsers = _userSuggestionsViewModel.GetUserSuggestions(userId, loadIgnoredSuggestions);
            Assert.Equal(expectedResult, suggestedUsers.Count());
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("MockId-1")]
        [InlineData("MockId-2")]
        public void IgnoreUserSuggestion(string userId)
        {
            var userSuggestion = new UserSuggestion { UserId1 = userId };
            var exception = Record.Exception(() => _userSuggestionsViewModel.IgnoreUserSuggestion(userSuggestion));
            Assert.Null(exception);
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("MockId-1", "MockId-1-Contact")]
        [InlineData("MockId-2", "MockId-2-Contact")]
        [InlineData("MockId-3", "MockId-3-Contact")]
        public void AddToContact(string userId, string contactId)
        {
            var exception = Record.Exception(() => _userSuggestionsViewModel.AddToContact(userId, contactId));
            Assert.Null(exception);
        }
    }
}
