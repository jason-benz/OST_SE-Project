using MediaHub.Data.FeedModule.Model;
using System.Collections.Generic;

namespace MediaHub.Test.FeedTest
{
    public class FeedServiceMock : IFeedService
    {
        private readonly IFeedDataManager _feedDataManager;
        private readonly Dictionary<string, Table> _filterStringToTableMap = new()
        {
            { "Media Ratings", Table.MediaRating },
            { "User profile update", Table.UserProfile }
        };

        public FeedServiceMock(IFeedDataManager feedDataManager)
        {
            _feedDataManager = feedDataManager;
        }

        public void AddToFeed(string userId, Table table, string? additionalInformation = null)
        {
            // No action required in the mock
        }

        public IEnumerable<FeedItem> LoadAllFeedItems(string userId)
        {
            var userIds = new List<string> { userId };
            return _feedDataManager.LoadAllFeedItems(userIds);
        }

        public IEnumerable<FeedItem> LoadFilteredFeedItems(string userId, Dictionary<string, bool> filterSettings)
        {
            var userIds = new List<string> { userId };
            var selectedTables = new List<Table>();
            
            foreach (var filterSetting in filterSettings)
            {
                if (filterSetting.Value)
                {
                    selectedTables.Add(_filterStringToTableMap[filterSetting.Key]);
                }
            }

            return _feedDataManager.LoadFilteredFeedItems(userIds, selectedTables);
        }
    }
}
