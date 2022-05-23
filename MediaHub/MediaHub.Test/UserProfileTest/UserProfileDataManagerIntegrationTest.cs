using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using MediaHub.Data.ProfileModule.Persistency;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MediaHub.Test.UserProfileTest
{
    public class UserProfileDataManagerIntegrationTest
    {
        private readonly IUserProfileDataManager _userProfileDataManager = new UserProfileDataManager();

        [Fact]
        public void GetUserProfileById()
        {
            var expectedUserProfile = InsertTestUserProfileInDB();
            var actualUserProfile = _userProfileDataManager.GetUserProfileById(expectedUserProfile.UserId);
            RemoveTestUserProfileFromDB(expectedUserProfile);

            Assert.Equal(expectedUserProfile, actualUserProfile);
        }

        [Fact]
        public void GetUserProfileByIdLazyLoading()
        {
            var expectedUserProfile = InsertTestUserProfileInDB();
            var actualUserProfile = _userProfileDataManager.GetUserProfileByIdLazyLoading(expectedUserProfile.UserId);
            RemoveTestUserProfileFromDB(expectedUserProfile);

            Assert.Equal(expectedUserProfile, actualUserProfile);
            Assert.Null(actualUserProfile.Ratings);
        }

        [Fact]
        public void GetUserProfileByIdNoTracking()
        {
            var expectedUserProfile = InsertTestUserProfileInDB();
            var actualUserProfile = _userProfileDataManager.GetUserProfileByIdNoTracking(expectedUserProfile.UserId);
            RemoveTestUserProfileFromDB(expectedUserProfile);

            Assert.Equal(expectedUserProfile, actualUserProfile);
        }

        [Fact]
        public void GetUserProfileByUsername()
        {
            var expectedUserProfile = InsertTestUserProfileInDB();
            var actualUserProfile = _userProfileDataManager.GetUserProfileByUsername(expectedUserProfile.Username);
            RemoveTestUserProfileFromDB(expectedUserProfile);

            Assert.Equal(expectedUserProfile, actualUserProfile);
        }

        [Fact]
        public void GetUserProfilesById_Single()
        {
            var expectedUserProfile = InsertTestUserProfileInDB();
            var userIds = new List<string> { expectedUserProfile.UserId };

            var actualUserProfiles = _userProfileDataManager.GetUserProfilesById(userIds);
            RemoveTestUserProfileFromDB(expectedUserProfile);

            Assert.Single(actualUserProfiles);
            Assert.Equal(expectedUserProfile, actualUserProfiles.First());
        }

        [Fact]
        public void GetUserProfilesById_Multiple()
        {
            var expectedUserProfile1 = InsertTestUserProfileInDB();
            var expectedUserProfile2 = InsertTestUserProfileInDB();
            var userIds = new List<string> { expectedUserProfile1.UserId, expectedUserProfile2.UserId };

            var actualUserProfiles = _userProfileDataManager.GetUserProfilesById(userIds);
            RemoveTestUserProfileFromDB(expectedUserProfile1);
            RemoveTestUserProfileFromDB(expectedUserProfile2);

            Assert.True(actualUserProfiles.Count() == 2);
        }

        [Fact]
        public void UpdateUserProfile()
        {
            var newUsername = "Name: UpdateUserProfile Test";
            var expectedUserProfile = InsertTestUserProfileInDB();
            expectedUserProfile.Username = newUsername;

            _userProfileDataManager.UpdateUserProfile(expectedUserProfile);
            var actualUserProfile = _userProfileDataManager.GetUserProfileByUsername(newUsername);
            RemoveTestUserProfileFromDB(expectedUserProfile);

            Assert.True(actualUserProfile.Equals(expectedUserProfile));
        }

        [Fact]
        public void IsUsernameAvailable_True()
        {
            var isUsernameAvailable = _userProfileDataManager.IsUsernameAvailable(Guid.NewGuid().ToString());
            Assert.True(isUsernameAvailable);
        }

        [Fact]
        public void IsUsernameAvailable_False()
        {
            var userProfile = InsertTestUserProfileInDB();

            var isUsernameAvailable = _userProfileDataManager.IsUsernameAvailable(userProfile.Username);
            RemoveTestUserProfileFromDB(userProfile);

            Assert.False(isUsernameAvailable);
        }

        private UserProfile InsertTestUserProfileInDB()
        {
            var guid = Guid.NewGuid();
            var userProfile = new UserProfile($"Test-ID-{guid}")
            {
                Username = $"Name-{guid}",
                Biography = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. " +
                "Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, " +
                "purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                ProfilePicture = ProfilePicture.GetTestProfilePicture()
            };

            using MediaHubDBContext context = new();
            context.Add(userProfile);
            context.SaveChanges();
            return userProfile;
        }

        private void RemoveTestUserProfileFromDB(UserProfile userProfile)
        {
            using MediaHubDBContext context = new();
            context.UserProfiles.Remove(userProfile);
            context.SaveChanges();
        }
    }
}
