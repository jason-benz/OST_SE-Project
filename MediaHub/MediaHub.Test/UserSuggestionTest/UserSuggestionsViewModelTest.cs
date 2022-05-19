using MediaHub.Data.UserSuggestionModule.Model;
using MediaHub.Data.UserSuggestionModule.ViewModel;
using MediaHub.Test.ContactTest;
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
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("MockId-1", false, 0)]
        [InlineData("MockId-2", false, 1)]
        [InlineData("MockId-2", true, 2)]
        public void GetUserSuggestions(string userId, bool loadIgnoredSuggestions, int expectedResult)
        {
            var suggestedUsers = _userSuggestionsViewModel.GetUserSuggestions(userId, loadIgnoredSuggestions);
            Assert.Equal(expectedResult, suggestedUsers.Count());
        }

        [Fact, Trait("Category", "Unit")]
        public void IgnoreUserSuggestion()
        {
            var userSuggestion = new UserSuggestion();
            var exception = Record.Exception(() => _userSuggestionsViewModel.IgnoreUserSuggestion(userSuggestion));
            Assert.Null(exception);
        }
    }
}
