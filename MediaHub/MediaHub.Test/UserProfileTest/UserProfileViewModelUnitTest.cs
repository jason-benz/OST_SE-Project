using Xunit;
using MediaHub.Data.ProfileModule.ViewModel;
using MediaHub.Data.ProfileModule.Model;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Test.LogTests;
using MediaHub.Test.FeedTest;
using System;

namespace MediaHub.Test.UserProfileTest
{
    [Collection("Sequential")]
    public class UserProfileViewModelUnitTest
    {
        private readonly IUserProfileViewModel _userProfileViewModel;

        public UserProfileViewModelUnitTest()
        {
            ILogService.Singleton = new LogServiceMock();
            _userProfileViewModel = new UserProfileViewModel(new UserProfileDataManagerMock(), new FeedServiceMock(new FeedDataManagerMock()));
        }

        [Fact, Trait("Category", "Unit")]
        public void GetUserProfileById_Succesfull()
        {
            string userId = "MockId-1";
            UserProfile? userProfile = _userProfileViewModel.GetUserProfileById(userId);
            Assert.Equal(userId, userProfile?.UserId);
        }

        [Fact, Trait("Category", "Unit")]
        public void GetUserProfileById_Null()
        {
            string userId = "2";
            UserProfile? userProfile = _userProfileViewModel.GetUserProfileById(userId);
            Assert.Null(userProfile);
        }

        [Fact, Trait("Category", "Unit")]
        public void GetUserProfileByUsername_Successfull()
        {
            string username = "MockUser-1";
            UserProfile? userProfile = _userProfileViewModel.GetUserProfileByUsername(username);
            Assert.Equal(username, userProfile?.Username);
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("MockUser-2")]
        [InlineData("MockUser-3")]
        [InlineData("asdf wasd")]
        public void GetUserProfileByUsername_Null(string username)
        {
            UserProfile? userProfile = _userProfileViewModel.GetUserProfileByUsername(username);
            Assert.Null(userProfile);
        }

        [Fact, Trait("Category", "Unit")]
        public void UpdateUserProfile_WithoutException()
        {
            var userProfile = new UserProfile("MockId-1");
            userProfile.Biography = new string('a', 300);
            var exception = Record.Exception(() => _userProfileViewModel.UpdateUserProfile(userProfile));
            Assert.Null(exception);
        }

        [Fact, Trait("Category", "Unit")]
        public void UpdateUserProfile_WithException()
        {
            var userProfile = new UserProfile("MockId-2");
            var exception = Record.Exception(() => _userProfileViewModel.UpdateUserProfile(userProfile));
            Assert.NotNull(exception);
        }

        [Fact, Trait("Category", "Unit")]
        public void IsUsernameAvailable_True()
        {
            var username = "true";
            var isUsernameAvailable = _userProfileViewModel.IsUsernameAvailable(username);
            Assert.True(isUsernameAvailable);
        }

        [Fact, Trait("Category", "Unit")]
        public void IsUsernameAvailable_False()
        {
            var username = "some taken user";
            var isUsernameAvailable = _userProfileViewModel.IsUsernameAvailable(username);
            Assert.False(isUsernameAvailable);
        }
    }
}
