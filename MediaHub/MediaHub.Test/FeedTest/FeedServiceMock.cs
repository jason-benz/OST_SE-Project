using MediaHub.Data.FeedModule.Model;
using System.Collections.Generic;

namespace MediaHub.Test.FeedTest
{
    public class FeedServiceMock : IFeedService
    {
        public void AddToFeed(string userId, Table table, string? additionalInformation = null)
        {
            // No action required in the mock
        }

        public IEnumerable<FeedItem> LoadAllFeedItems(string userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<FeedItem> LoadFilteredFeedItems(string userId, Dictionary<string, bool> filterSettings)
        {
            throw new System.NotImplementedException();
        }
    }
}
