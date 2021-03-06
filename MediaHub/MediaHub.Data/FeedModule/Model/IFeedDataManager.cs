namespace MediaHub.Data.FeedModule.Model
{
    public interface IFeedDataManager
    {
        void AddFeedItem(FeedItem feedItem);

        bool IsItemExisting(string userId, Table table, string? additionalInformation);

        IEnumerable<FeedItem> LoadFilteredFeedItems(IEnumerable<string> userIds, IEnumerable<Table> selectedTables);
    }
}
