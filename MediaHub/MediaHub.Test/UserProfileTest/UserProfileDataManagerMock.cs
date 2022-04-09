using MediaHub.Data.Model;
using System;

namespace MediaHub.Test.UserProfileTest
{
    internal class UserProfileDataManagerMock : IUserProfileDataManager
    {
        public UserProfile GetUserProfileById(string userId)
        {
            return new UserProfile(userId)
            {
                Username = "Mock test username",
                Biography = "Mock test biography",
                ProfilePicture = ProfilePicture.GetTestProfilePicture()
            };
        }

        public UserProfile GetUserProfileByUsername(string username)
        {
            return new UserProfile(Guid.NewGuid().ToString())
            {
                Username = username,
                Biography = "Mock test biography of method 'GetUserProfileByUsername'",
                ProfilePicture = ProfilePicture.GetTestProfilePicture()
            };
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            // Method in mock is empty, because the single use case is to update the userprofile without return value
        }
    }
}
