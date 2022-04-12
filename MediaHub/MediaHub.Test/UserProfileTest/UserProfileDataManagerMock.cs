using MediaHub.Data.Model;
using System;
using System.Collections.Generic;

namespace MediaHub.Test.UserProfileTest
{
    internal class UserProfileDataManagerMock : IUserProfileDataManager
    {
        public UserProfile UpdatedUserProfile { get; set; }
        public MediaRating TestRating { get; set; } = null; 
        public UserProfile GetUserProfileById(string userId)
        {
            var userProfile = new UserProfile(userId)
            {
                Username = "Mock test username",
                Biography = "Mock test biography",
                Ratings = new List<MediaRating>(),
                ProfilePicture = ProfilePicture.GetTestProfilePicture()
            };
            if (TestRating != null)
            { 
                TestRating.Profile = userProfile;
                userProfile.Ratings.Add(TestRating);    
            }
            
            return userProfile;
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
            UpdatedUserProfile = userProfile;
        }
    }
}
