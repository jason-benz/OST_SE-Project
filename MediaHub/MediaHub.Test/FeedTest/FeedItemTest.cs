using MediaHub.Data.FeedModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MediaHub.Test.FeedTest
{
    public class FeedItemTest
    {
        [Theory, Trait("Category", "Unit")]
        [InlineData("user1", "user1", true)]
        [InlineData("user1", "user2", false)]
        public void FeedItemEquals(string userId1, string userId2, bool expectedResult)
        {
            var FeedItem1 = CreateFeedItem(userId1);
            var FeedItem2 = CreateFeedItem(userId2);
            Assert.Equal(FeedItem1.Equals(FeedItem2), expectedResult);
        }

        [Fact]
        public void FeedItemEquals_Null()
        {
            var feedItem1 = CreateFeedItem("Test");
            FeedItem? feedItem2 = null;
            Assert.False(feedItem1.Equals(feedItem2));
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("user1", "user1", true)]
        [InlineData("user1", "user2", false)]
        public void FeedItemGetHashCode(string userId1, string userId2, bool expectedResult)
        {
            var feedItem1 = CreateFeedItem(userId1);
            var feedItem2 = CreateFeedItem(userId2);

            var hashCode1 = feedItem1.GetHashCode();
            var hashCode2 = feedItem2.GetHashCode();

            Assert.Equal(hashCode1 == hashCode2, expectedResult);
        }

        private static FeedItem CreateFeedItem(string userId)
        {
            return new FeedItem(userId)
            {
                ChangedTable = Table.Feed,
                CreationDate = DateTime.Today,
                AdditionalInformation = "Feed Test"
            };
        }
    }
}
