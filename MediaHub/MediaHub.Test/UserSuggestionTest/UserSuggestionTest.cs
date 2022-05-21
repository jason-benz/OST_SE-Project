using MediaHub.Data.UserSuggestionModule.Model;
using Xunit;

namespace MediaHub.Test.UserSuggestionTest
{
    public class UserSuggestionTest
    {
        [Theory, Trait("Category", "Unit")]
        [InlineData("user1", "user1", true)]
        [InlineData("user1", "user2", false)]
        public void UserProfileEquals(string userId1, string userId2, bool expectedResult)
        {
            var userSuggestion1 = CreateUserSuggestion(userId1);
            var userSuggestion2 = CreateUserSuggestion(userId2);
            Assert.Equal(userSuggestion1.Equals(userSuggestion2), expectedResult);
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("user1", "user1", true)]
        [InlineData("user1", "user2", false)]
        public void UserSuggestionGetHashCode(string userId1, string userId2, bool expectedResult)
        {
            var userSuggestion1 = CreateUserSuggestion(userId1);
            var userSuggestion2 = CreateUserSuggestion(userId2);

            var hashCode1 = userSuggestion1.GetHashCode();
            var hashCode2 = userSuggestion2.GetHashCode();

            Assert.Equal(hashCode1 == hashCode2, expectedResult);
        }

        private static UserSuggestion CreateUserSuggestion(string userId)
        {
            return new UserSuggestion()
            {
                UserId1 = userId,
                UserId2 = userId,
                IgnoreSuggestion = false
            };
        }
    }
}
