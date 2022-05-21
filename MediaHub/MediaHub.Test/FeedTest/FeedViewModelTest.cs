using MediaHub.Data.FeedModule.Model;
using MediaHub.Data.FeedModule.ViewModel;
using System.Linq;
using Xunit;

namespace MediaHub.Test.FeedTest
{
    public class FeedViewModelTest
    {
        private readonly IFeedViewModel _feedViewModel;
        
        public FeedViewModelTest()
        {
             _feedViewModel = new FeedViewModel(new FeedServiceMock(new FeedDataManagerMock()));
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData(Table.UserProfile, "", "The user profile has been updated.")]
        [InlineData(Table.UserProfile, "Test", "The user profile has been updated.")]
        [InlineData(Table.MediaRating, "Avengers", "An interaction was made with the movie Avengers")]
        [InlineData(Table.MediaRating, "", "An interaction was made with the movie ")]
        [InlineData(Table.Feed, "", "")]
        [InlineData(Table.Feed, "asdf", "")]
        [InlineData(Table.Message, "", "")]
        [InlineData(Table.Message, "test", "")]
        [InlineData(Table.UserSuggestion, "", "")]
        [InlineData(Table.UserSuggestion, "wasd", "")]
        public void LoadFeedDescription(Table changedTable, string? additionalInfo, string expectedDescription)
        {
            var actualDescription = _feedViewModel.LoadFeedDescription(changedTable, additionalInfo);
            Assert.Equal(expectedDescription, actualDescription);
        }

        [Fact, Trait("Category", "Unit")]
        public void LoadFeedItems()
        {
            var userId = "Test";
            _feedViewModel.UserId = userId;
            _feedViewModel.LoadFeedItems();
            var feedItems = _feedViewModel.FeedItems;

            Assert.True(feedItems.Any());
            Assert.Equal(userId, feedItems.First().UserId);
        }

        [Fact, Trait("Category", "Unit")]
        public void FilterBarChange()
        {
            // Inverts UserProfileUpdate filter, therefore only MediaRating should be loaded
            _feedViewModel.FilterbarViewModel.OnChange("User profile update");
            var feedItems = _feedViewModel.FeedItems;

            Assert.True(feedItems.Any());
            Assert.Equal(Table.MediaRating, feedItems.First().ChangedTable);
        }
    }
}
