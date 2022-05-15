using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using MediaHub.Data.UserSuggestionModule.Model;
using MediaHub.Data.UserSuggestionModule.Persistency;
using System;
using System.Linq;
using Xunit;

namespace MediaHub.Test.UserSuggestionTest
{
    public class UserSuggestionDataManagerIntegrationTest
    {
        private readonly IUserSuggestionDataManager _userSuggestionDataManager = new UserSuggestionDataManager();

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetSuggestedUsersLazyLoading(bool loadIgnoredSuggestion)
        {
            var expectedUserSuggestion = InsertTestUserSuggestionInDB(loadIgnoredSuggestion);
            var actualUserSuggestion = _userSuggestionDataManager.GetSuggestedUsersLazyLoading(expectedUserSuggestion.UserId1, loadIgnoredSuggestion).First();
            RemoveTestUserSuggestionAndProfileFromDB(expectedUserSuggestion);

            Assert.Equal(expectedUserSuggestion, actualUserSuggestion);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetSuggestedUsers(bool loadIgnoredSuggestion)
        {
            var expectedUserSuggestion = InsertTestUserSuggestionInDB(loadIgnoredSuggestion);
            var actualUserSuggestion = _userSuggestionDataManager.GetSuggestedUsers(expectedUserSuggestion.UserId1, loadIgnoredSuggestion).First();
            RemoveTestUserSuggestionAndProfileFromDB(expectedUserSuggestion);

            Assert.Equal(expectedUserSuggestion, actualUserSuggestion);
        }

        [Fact]
        public void AddUserSuggestion()
        {
            var expectedUserSuggestion = CreateUserSuggestion(false);
            _userSuggestionDataManager.AddUserSuggestion(expectedUserSuggestion);
            var actualUserSuggestion = _userSuggestionDataManager.GetSuggestedUsersLazyLoading(expectedUserSuggestion.UserId1).First();
            RemoveTestUserSuggestionAndProfileFromDB(expectedUserSuggestion);

            Assert.Equal(expectedUserSuggestion, actualUserSuggestion);
        }

        [Fact]
        public void UpdateUserSuggestion()
        {
            var expectedUserSuggestion = InsertTestUserSuggestionInDB(false);
            
            expectedUserSuggestion.IgnoreSuggestion = true;
            _userSuggestionDataManager.UpdateUserSuggestion(expectedUserSuggestion);
            var actualUserSuggestion = _userSuggestionDataManager.GetSuggestedUsersLazyLoading(expectedUserSuggestion.UserId1).First();
            RemoveTestUserSuggestionAndProfileFromDB(expectedUserSuggestion);

            Assert.Equal(expectedUserSuggestion, actualUserSuggestion);
        }

        private UserSuggestion InsertTestUserSuggestionInDB(bool ignoreSuggestion)
        {
            var userSuggestion = CreateUserSuggestion(ignoreSuggestion);

            using MediaHubDBContext context = new();
            context.Add(userSuggestion);
            context.SaveChanges();
            return userSuggestion;
        }

        private UserSuggestion CreateUserSuggestion(bool ignoreSuggestion)
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            var userProfile1 = new UserProfile(guid1.ToString());
            var userProfile2 = new UserProfile(guid2.ToString());

            var userSuggestion = new UserSuggestion
            {
                UserId1 = guid1.ToString(),
                UserProfile1 = userProfile1,
                UserId2 = guid2.ToString(),
                UserProfile2 = userProfile2,
                IgnoreSuggestion = ignoreSuggestion
            };

            return userSuggestion;
        }

        private void RemoveTestUserSuggestionAndProfileFromDB(UserSuggestion userSuggestion)
        {
            using MediaHubDBContext context = new();
            context.UserSuggestions.Remove(userSuggestion);
            context.UserProfiles.Remove(userSuggestion.UserProfile1);
            context.UserProfiles.Remove(userSuggestion.UserProfile2);
            context.SaveChanges();
        }
    }
}
