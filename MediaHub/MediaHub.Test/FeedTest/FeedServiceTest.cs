using MediaHub.Data.FeedModule.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MediaHub.Test.FeedTest
{
    public class FeedServiceTest
    {
        private readonly IFeedService _feedService;

        public FeedServiceTest()
        {
            _feedService = new FeedService(new FeedDataManagerMock());
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("MockId-1", Table.UserProfile)]
        [InlineData("MockId-1", Table.MediaRating)]
        [InlineData("MockId-2", Table.UserProfile)]
        [InlineData("MockId-2", Table.MediaRating)]
        public void AddToFeed(string userId, Table table)
        {
            var exception = Record.Exception(() => _feedService.AddToFeed(userId, table));
            Assert.Null(exception);
        }

        /// <summary>
        /// It fails at the moment and this is correct! Contact integration required! (issue #57)
        /// </summary>
        [Theory, Trait("Category", "Unit")]
        [InlineData("MockId-1")]
        [InlineData("MockId-2")]
        public void LoadAllFeedItems(string userId)
        {
            var feedItems = _feedService.LoadAllFeedItems(userId);
            Assert.True(feedItems.Any());
            Assert.Equal(userId, feedItems.First().UserId);
        }

        /// <summary>
        /// It fails at the moment and this is correct! Contact integration required! (issue #57)
        /// </summary>
        [Theory, Trait("Category", "Unit")]
        [InlineData("Media Ratings", true, Table.MediaRating)]
        [InlineData("User profile update", true, Table.UserProfile)]
        public void LoadfilteredFeedItems_FilterTrue(string filterName, bool filterApplied, Table expectedTable)
        {
            var userId = "MockId-1";
            var filter = new Dictionary<string, bool> { { filterName, filterApplied } };
            var feedItems = _feedService.LoadFilteredFeedItems(userId, filter);
            Assert.True(feedItems.Any());
            Assert.Equal(userId, feedItems.First().UserId);
            Assert.Equal(expectedTable, feedItems.First().ChangedTable);
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("Media Ratings", true, Table.MediaRating)]
        [InlineData("User profile update", true, Table.UserProfile)]
        public void LoadFilteredFeedItems_FilterFalse(string filterName, bool filterApplied, Table expectedTable)
        {
            var userId = "MockId-1";
            var filter = new Dictionary<string, bool> { { filterName, filterApplied } };
            var feedItems = _feedService.LoadFilteredFeedItems(userId, filter);
            Assert.False(feedItems.Any());
        }

        [Fact, Trait("Category", "Unit")]
        public void LoadFilteredFeedItems_WithoutResult()
        {
            var userId = "MockId-1";
            var filter = new Dictionary<string, bool> { { "Weird stuff", true } };
            var feedItems = _feedService.LoadFilteredFeedItems(userId, filter);
            Assert.False(feedItems.Any());
        }
    }
}
