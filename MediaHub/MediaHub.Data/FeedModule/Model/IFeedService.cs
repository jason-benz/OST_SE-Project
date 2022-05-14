namespace MediaHub.Data.FeedModule.Model
{
    public interface IFeedService
    {
        void AddToFeed(string userId, Table table, string? additionalInformation = null);

        IEnumerable<FeedItem> LoadAllFeedItems(string userId);

        IEnumerable<FeedItem> LoadFilteredFeedItems(string userId, Dictionary<string, bool> filterSettings);
    }
}
