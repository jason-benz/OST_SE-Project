using MediaHub.Data.ContactsModule.Model;

namespace MediaHub.Data.FeedModule.Model
{
    public class FeedService : IFeedService
    {
        private readonly IFeedDataManager _feedDataManager;
        private readonly IContactDataManager _contactDataManager;
        private readonly Dictionary<string, Table> _filterStringToTableMap = new()
        {
            { "Media Ratings", Table.MediaRating },
            { "User profile update", Table.UserProfile }
        };

        public FeedService(IFeedDataManager feedDataManager, IContactDataManager contactDataManager)
        {
            _feedDataManager = feedDataManager;
            _contactDataManager = contactDataManager;
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

        public IEnumerable<FeedItem> LoadFilteredFeedItems(string userId, Dictionary<string, bool> filterSettings)
        {
            var contactIds = _contactDataManager.GetContactIds(userId);
            var selectedTables = new List<Table>();
            foreach (var filterSetting in filterSettings)
            {
                if (filterSetting.Value && _filterStringToTableMap.ContainsKey(filterSetting.Key))
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
