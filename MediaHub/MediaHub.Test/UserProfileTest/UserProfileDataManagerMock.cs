using MediaHub.Data.Model;
using System;

namespace MediaHub.Test.UserProfileTest
{
    internal class UserProfileDataManagerMock : IUserProfileDataManager
    {
        public UserProfile? GetUserProfileById(string userId)
        {
            switch (userId)
            {
                case "1":
                    return new UserProfile(userId)
                    {
                        Username = "Mock test username",
                        Biography = "Mock test biography",
                        ProfilePicture = ProfilePicture.GetTestProfilePicture()
                    };
                case "2":
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(userId));
            }
            
        }

        public UserProfile? GetUserProfileByUsername(string username)
        {
            switch (username)
            {
                case "MockUser-1":
                    return new UserProfile(Guid.NewGuid().ToString())
                    {
                        Username = username,
                        Biography = "Mock test biography of method 'GetUserProfileByUsername'",
                        ProfilePicture = ProfilePicture.GetTestProfilePicture()
                    };
                case "MockUser-2":
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(username));
            }
        }

        public bool IsUsernameAvailable(string username)
        {
            if (username == "true")
            {
                return true;
            }

            return false;
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            switch (userProfile.UserId)
            {
                case "MockId-1":
                    break;
                case "MockId-2":
                    throw new Exception();
                default:
                    throw new ArgumentOutOfRangeException(nameof(userProfile));
            }
        }
    }
}
