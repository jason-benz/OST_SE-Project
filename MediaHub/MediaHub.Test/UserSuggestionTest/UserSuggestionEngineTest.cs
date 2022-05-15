using MediaHub.Data.UserSuggestionModule.Model;
using System.Threading;
using Xunit;

namespace MediaHub.Test.UserSuggestionTest
{
    public class UserSuggestionEngineTest
    {
        private readonly IUserSuggestionEngine _userSuggestionEngine;
        private readonly UserSuggestionDataManagerMock _userSuggestionDataManager;

        public UserSuggestionEngineTest()
        {
            _userSuggestionDataManager = new UserSuggestionDataManagerMock();
            _userSuggestionEngine = new UserSuggestionEngine(_userSuggestionDataManager);
        }

        [Fact, Trait("Category", "Unit")]
        public void StartUserSuggestionEngine()
        {
            string userId = "MockId-1";
            _userSuggestionEngine.StartUserSuggestionEngine(userId);
            Thread.Sleep(250);
            Assert.Equal(userId, _userSuggestionDataManager.UserId);
        }
    }
}
