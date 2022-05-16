namespace MediaHub.Data.FeedModule.Model
{
    public class FeedService : IFeedService
    {
        private readonly IFeedDataManager _feedDataManager;
        private readonly Dictionary<string, Table> _filterStringToTableMap = new()
        {
            { "Media Ratings", Table.MediaRating },
            { "User profile update", Table.UserProfile }
        };

        public FeedService(IFeedDataManager feedDataManager)
        {
            _feedDataManager = feedDataManager;
        }

        public void AddToFeed(string userId, Table table, string? additionalInformation = null)
        {
            if (IsItemExisting(userId, table, additionalInformation))
            {
                return;
            }

            var feedItem = new FeedItem(userId)
            {
                ChangedTable = table,
                AdditionalInformation = additionalInformation
            };

            _feedDataManager.AddFeedItem(feedItem);
        }

        public IEnumerable<FeedItem> LoadAllFeedItems(string userId)
        {
            var contactIds = new List<string>(); // TODO: Load contacts from DB
            return _feedDataManager.LoadAllFeedItems(contactIds);
        }

        public IEnumerable<FeedItem> LoadFilteredFeedItems(string userId, Dictionary<string, bool> filterSettings)
        {
            var contactIds = new List<string>(); // TODO: Load contacts from DB
            var selectedTables = new List<Table>();
            foreach(var filterSetting in filterSettings)
            {
                if (filterSetting.Value)
                {
                    selectedTables.Add(_filterStringToTableMap[filterSetting.Key]);
                }
            }
            return _feedDataManager.LoadFilteredFeedItems(contactIds, selectedTables);
        }

        private bool IsItemExisting(string userId, Table table, string? additionalInformation)
        {
            return _feedDataManager.IsItemExisting(userId, table, additionalInformation);
        }
    }
}
