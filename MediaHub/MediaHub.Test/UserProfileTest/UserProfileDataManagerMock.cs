using MediaHub.Data.Model;
using System;
using System.Collections.Generic;

namespace MediaHub.Test.UserProfileTest
{
    internal class UserProfileDataManagerMock : IUserProfileDataManager
    {
        public UserProfile UpdatedUserProfile { get; set; }
        public MediaRating TestRating { get; set; } = null; 
        public UserProfile? GetUserProfileById(string userId)
        {
            switch (userId)
            {
                case "1":
                    var profile = new UserProfile(userId)
                                    {
                                        Username = "Mock test username",
                                        Biography = "Mock test biography",
                                        Ratings = new List<MediaRating>(),
                                        ProfilePicture = ProfilePicture.GetTestProfilePicture()
                                    };
                    if (TestRating != null)
                    {
                        TestRating.Profile = profile;
                        profile.Ratings.Add(TestRating);
                    }

                    return profile;
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
            UpdatedUserProfile = userProfile;
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
