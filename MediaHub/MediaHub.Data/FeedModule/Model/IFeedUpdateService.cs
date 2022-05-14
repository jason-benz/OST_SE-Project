namespace MediaHub.Data.FeedModule.Model
{
    public interface IFeedUpdateService
    {
        void AddToFeed(string userId, Table table, string? additionalInformation = null);
    }
}
