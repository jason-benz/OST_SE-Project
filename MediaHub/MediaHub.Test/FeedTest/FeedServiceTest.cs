using MediaHub.Data.FeedModule.Model;
using MediaHub.Test.ContactTest;
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
            _feedService = new FeedService(new FeedDataManagerMock(), new ContactDataManagerMock());
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

        [Theory, Trait("Category", "Unit")]
        [InlineData("Media Ratings", true, Table.MediaRating)]
        [InlineData("User profile update", true, Table.UserProfile)]
        public void LoadfilteredFeedItems_FilterTrue(string filterName, bool filterApplied, Table expectedTable)
        {
            var userId = "MockId-1";
            var contactId = "MockId-1-Contact";

            var filter = new Dictionary<string, bool> { { filterName, filterApplied } };
            var feedItems = _feedService.LoadFilteredFeedItems(userId, filter);
            Assert.True(feedItems.Any());
            Assert.Equal(contactId, feedItems.First().UserId);
            Assert.Equal(expectedTable, feedItems.First().ChangedTable);
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("Media Ratings", false)]
        [InlineData("User profile update", false)]
        public void LoadFilteredFeedItems_FilterFalse(string filterName, bool filterApplied)
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
