using MediaHub.Data.Model;
using MediaHub.Data.ViewModel;
using Xunit;

namespace MediaHub.Test.UserProfileTest
{
    public class UserProfileViewModelUnitTest
    {
        private readonly IUserProfileViewModel _userProfileViewModel;

        public UserProfileViewModelUnitTest()
        {
            _userProfileViewModel = new UserProfileViewModel(new UserProfileDataManagerMock());            
        }

        [Fact]
        public void GetUserProfileById()
        {
            string userId = "123";
            UserProfile userProfile = _userProfileViewModel.GetUserProfileById(userId);
            Assert.Equal(userId, userProfile.UserId);
        }

        [Fact]
        public void GetUserProfileByUsername()
        {
            string username = "MockUser";
            UserProfile userProfile = _userProfileViewModel.GetUserProfileByUsername(username);
            Assert.Equal(username, userProfile.Username);
        }

        [Fact]
        public void UpdateUserProfileWithoutException()
        {
            var userProfile = new UserProfile("Test ID");
            _userProfileViewModel.UpdateUserProfile(userProfile);
            var exception = Record.Exception(() => new UserProfileViewModelUnitTest());
            Assert.Null(exception);
        }
    }
}
