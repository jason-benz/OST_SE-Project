using MediaHub.Data.FeedModule.Model;
using System;
using System.Collections.Generic;

namespace MediaHub.Test.FeedTest
{
    public class FeedDataManagerMock : IFeedDataManager
    {
        public void AddFeedItem(FeedItem feedItem)
        {
            throw new NotImplementedException();
        }

        public bool IsItemExisting(string userId, Table table, string? additionalInformation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FeedItem> LoadFilteredFeedItems(IEnumerable<string> userIds, IEnumerable<Table> selectedTables)
        {
            throw new NotImplementedException();
        }
    }
}
