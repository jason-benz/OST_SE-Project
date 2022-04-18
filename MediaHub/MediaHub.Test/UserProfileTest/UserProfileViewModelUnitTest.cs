using MediaHub.Data.Model;
using MediaHub.Data.ViewModel;
using System;
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
        public void GetUserProfileById_Succesfull()
        {
            string userId = "1";
            UserProfile? userProfile = _userProfileViewModel.GetUserProfileById(userId);
            Assert.Equal(userId, userProfile?.UserId);
        }

        [Fact]
        public void GetUserProfileById_Null()
        {
            string userId = "2";
            UserProfile? userProfile = _userProfileViewModel.GetUserProfileById(userId);
            Assert.Null(userProfile);
        }

        [Fact]
        public void GetUserProfileByUsername_Successfull()
        {
            string username = "MockUser-1";
            UserProfile? userProfile = _userProfileViewModel.GetUserProfileByUsername(username);
            Assert.Equal(username, userProfile?.Username);
        }

        [Fact]
        public void GetUserProfileByUsername_Null()
        {
            string username = "MockUser-2";
            UserProfile? userProfile = _userProfileViewModel.GetUserProfileByUsername(username);
            Assert.Null(userProfile);
        }

        [Fact]
        public void UpdateUserProfile_WithoutException()
        {
            var userProfile = new UserProfile("MockId-1");
            var exception = Record.Exception(() => _userProfileViewModel.UpdateUserProfile(userProfile));
            Assert.Null(exception);
        }

        [Fact]
        public void UpdateUserProfile_WithException()
        {
            var userProfile = new UserProfile("MockId-2");
            var exception = Record.Exception(() => _userProfileViewModel.UpdateUserProfile(userProfile));
            Assert.NotNull(exception);
        }

        [Fact]
        public void IsUsernameAvailable_True()
        {
            var username = "true";
            var isUsernameAvailable = _userProfileViewModel.IsUsernameAvailable(username);
            Assert.True(isUsernameAvailable);
        }

        [Fact]
        public void IsUsernameAvailable_False()
        {
            var username = "some taken user";
            var isUsernameAvailable = _userProfileViewModel.IsUsernameAvailable(username);
            Assert.False(isUsernameAvailable);
        }
    }
}
