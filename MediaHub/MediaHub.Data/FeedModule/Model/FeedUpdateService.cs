namespace MediaHub.Data.FeedModule.Model
{
    public class FeedUpdateService : IFeedUpdateService
    {
        private readonly IFeedDataManager _feedDataManager;

        public FeedUpdateService(IFeedDataManager feedDataManager)
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

        private bool IsItemExisting(string userId, Table table, string? additionalInformation)
        {
            return _feedDataManager.IsItemExisting(userId, table, additionalInformation);
        }     
    }
}
