using MediaHub.Data.ProfileModule.Model;
using Xunit;

namespace MediaHub.Test.UserProfileTest
{
    public class UserProfileTest
    {
        [Theory, Trait("Category", "Unit")]
        [InlineData("user1", "user1", true)]
        [InlineData("user1", "user2", false)]
        public void UserProfileEquals(string userId1, string userId2, bool expectedResult)
        {
            var userProfile1 = CreateUserProfile(userId1);
            var userProfile2 = CreateUserProfile(userId2);
            Assert.Equal(userProfile1.Equals(userProfile2), expectedResult);
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("user1", "user1", true)]
        [InlineData("user1", "user2", false)]
        public void UserProfileGetHashCode(string userId1, string userId2, bool expectedResult)
        {
            var userProfile1 = CreateUserProfile(userId1);
            var userProfile2 = CreateUserProfile(userId2);

            var hashCode1 = userProfile1.GetHashCode();
            var hashCode2 = userProfile2.GetHashCode();

            Assert.Equal(hashCode1 == hashCode2, expectedResult);
        }

        private static UserProfile CreateUserProfile(string userId)
        {
            return new UserProfile(userId)
            {
                Username = "Test name",
                Biography = "Test bio",
                ProfilePicture = ProfilePicture.GetTestProfilePicture()
            };
        }
    }
}
