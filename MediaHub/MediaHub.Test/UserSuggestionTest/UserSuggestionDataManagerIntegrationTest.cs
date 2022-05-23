using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using MediaHub.Data.UserSuggestionModule.Model;
using MediaHub.Data.UserSuggestionModule.Persistency;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MediaHub.Test.UserSuggestionTest
{
    public class UserSuggestionDataManagerIntegrationTest
    {
        private readonly IUserSuggestionDataManager _userSuggestionDataManager = new UserSuggestionDataManager();

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetSuggestedUsersLazyLoading(bool loadIgnoredSuggestion)
        {
            var expectedUserSuggestion = InsertTestUserSuggestionInDB(loadIgnoredSuggestion, false);
            var actualUserSuggestion = _userSuggestionDataManager.GetSuggestedUsersLazyLoading(expectedUserSuggestion.UserId1, loadIgnoredSuggestion).First();
            RemoveTestUserSuggestionAndProfileFromDB(expectedUserSuggestion);

            Assert.Equal(expectedUserSuggestion, actualUserSuggestion);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetSuggestedUsers(bool loadIgnoredSuggestion)
        {
            var expectedUserSuggestion = InsertTestUserSuggestionInDB(loadIgnoredSuggestion, false);
            var actualUserSuggestion = _userSuggestionDataManager.GetSuggestedUsers(expectedUserSuggestion.UserId1, loadIgnoredSuggestion).First();
            RemoveTestUserSuggestionAndProfileFromDB(expectedUserSuggestion);

            Assert.Equal(expectedUserSuggestion, actualUserSuggestion);
        }

        [Fact]
        public void GetLikedMovieIdsByUserId_Empty()
        {
            var expectedUserSuggestion = InsertTestUserSuggestionInDB(false, false);
            
            var actualMovieIds = _userSuggestionDataManager.GetLikedMovieIdsByUserId(expectedUserSuggestion.UserId1);
            RemoveTestUserSuggestionAndProfileFromDB(expectedUserSuggestion);

            Assert.Empty(actualMovieIds);

        }

        [Fact]
        public void GetLikedMovieIdsByUserId()
        {
            var expectedUserSuggestion = InsertTestUserSuggestionInDB(false, true);

            var actualMovieIds = _userSuggestionDataManager.GetLikedMovieIdsByUserId(expectedUserSuggestion.UserId1);
            RemoveTestUserSuggestionAndProfileFromDB(expectedUserSuggestion);

            Assert.True(actualMovieIds.Count() == expectedUserSuggestion.UserProfile1.Ratings.Count());
            Assert.Equal(expectedUserSuggestion.UserProfile1.Ratings.Select(r => r.MovieId), actualMovieIds);
        }

        [Fact]
        public void GetUserIdsToBeSuggested_Empty()
        {
            var movieIds = new List<int>();
            var usersToIgnore = new List<string>();

            var userIdsToBeSuggested = _userSuggestionDataManager.GetUserIdsToBeSuggested(movieIds, usersToIgnore);

            Assert.Empty(userIdsToBeSuggested);
        }

        [Fact]
        public void GetUserIdsToBeSuggested()
        {
            var expectedUserSuggestion = InsertTestUserSuggestionInDB(false, true);
            var expectedMovieIds = expectedUserSuggestion.UserProfile1.Ratings.Select(r => r.MovieId);
            var usersToIgnore = new List<string>();

            var userIdsToBeSuggested = _userSuggestionDataManager.GetUserIdsToBeSuggested(expectedMovieIds, usersToIgnore);
            RemoveTestUserSuggestionAndProfileFromDB(expectedUserSuggestion);

            Assert.True(userIdsToBeSuggested.Count() == 2);
            Assert.Contains(expectedUserSuggestion.UserId1, userIdsToBeSuggested);
            Assert.Contains(expectedUserSuggestion.UserId2, userIdsToBeSuggested);
        }

        [Fact]
        public void AddUserSuggestion()
        {
            var expectedUserSuggestion = CreateUserSuggestion(false, false);
            _userSuggestionDataManager.AddUserSuggestion(expectedUserSuggestion);
            var actualUserSuggestion = _userSuggestionDataManager.GetSuggestedUsersLazyLoading(expectedUserSuggestion.UserId1).First();
            RemoveTestUserSuggestionAndProfileFromDB(expectedUserSuggestion);

            Assert.Equal(expectedUserSuggestion, actualUserSuggestion);
        }

        [Fact]
        public void UpdateUserSuggestion()
        {
            var expectedUserSuggestion = InsertTestUserSuggestionInDB(false, false);
            
            expectedUserSuggestion.IgnoreSuggestion = true;
            _userSuggestionDataManager.UpdateUserSuggestion(expectedUserSuggestion);
            var actualUserSuggestion = _userSuggestionDataManager.GetSuggestedUsersLazyLoading(expectedUserSuggestion.UserId1).First();
            RemoveTestUserSuggestionAndProfileFromDB(expectedUserSuggestion);

            Assert.Equal(expectedUserSuggestion, actualUserSuggestion);
        }

        private UserSuggestion InsertTestUserSuggestionInDB(bool ignoreSuggestion, bool addRating)
        {
            var userSuggestion = CreateUserSuggestion(ignoreSuggestion, addRating);

            using MediaHubDBContext context = new();
            context.Add(userSuggestion);
            context.SaveChanges();
            return userSuggestion;
        }

        private MediaRating GetMediaRating(string userId, int movieId)
        {
            return new MediaRating()
            {
                ProfileId = userId,
                MovieId = movieId,
                IsAddedToProfile = true
            };
        }

        private UserSuggestion CreateUserSuggestion(bool ignoreSuggestion, bool addRating)
        {
            var guid1 = Guid.NewGuid().ToString();
            var guid2 = Guid.NewGuid().ToString();

            var userProfile1 = new UserProfile(guid1);
            var userProfile2 = new UserProfile(guid2);

            if (addRating)
            {
                for (int i = 0; i < 10; i++)
                {
                    var movieId = new Random().Next(1, 100);

                    var mediaRating1 = GetMediaRating(guid1, movieId);
                    userProfile1.Ratings.Add(mediaRating1);

                    var mediaRating2 = GetMediaRating(guid2, movieId);
                    userProfile2.Ratings.Add(mediaRating2);
                }
            }

            var userSuggestion = new UserSuggestion
            {
                UserId1 = guid1.ToString(),
                UserProfile1 = userProfile1,
                UserId2 = guid2.ToString(),
                UserProfile2 = userProfile2,
                IgnoreSuggestion = ignoreSuggestion,
            };

            return userSuggestion;
        }

        private void RemoveTestUserSuggestionAndProfileFromDB(UserSuggestion userSuggestion)
        {
            using MediaHubDBContext context = new();
            context.UserSuggestions.Remove(userSuggestion);
            context.UserProfiles.Remove(userSuggestion.UserProfile1);
            context.UserProfiles.Remove(userSuggestion.UserProfile2);
            context.Ratings.RemoveRange(userSuggestion.UserProfile1.Ratings);
            context.Ratings.RemoveRange(userSuggestion.UserProfile2.Ratings);
            context.SaveChanges();
        }
    }
}
