using MediaHub.Data.FeedModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<FeedItem> LoadAllFeedItems(IEnumerable<string> userIds)
        {
            List<FeedItem> feedItems = new();

            foreach (var userId in userIds)
            {
                feedItems.Add(new FeedItem(userId));
            }

            return feedItems;
        }

        public IEnumerable<FeedItem> LoadFilteredFeedItems(IEnumerable<string> userIds, IEnumerable<Table> selectedTables)
        {
            List<FeedItem> feedItems = new List<FeedItem>();

            foreach (var userId in userIds)
            {
                var feedItem = new FeedItem(userId);

                if (selectedTables != null && selectedTables.Any())
                {
                    feedItem.ChangedTable = selectedTables.First();
                }

                feedItems.Add(feedItem);
            }

            return feedItems;
        }
    }
}
