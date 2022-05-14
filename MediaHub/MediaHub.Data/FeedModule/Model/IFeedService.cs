namespace MediaHub.Data.FeedModule.Model
{
    public interface IFeedService
    {
        void AddToFeed(string userId, Table table, string? additionalInformation = null);
    }
}
