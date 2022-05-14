using MediaHub.Data.FeedModule.Model;

namespace MediaHub.Data.FeedModule.ViewModel
{
    public interface IFeedViewModel
    {
        IEnumerable<FeedItem> LoadAllFeedItems(string userId);

        IEnumerable<FeedItem> LoadFilteredFeedItems(string userId, Dictionary<string, bool> filterSettings);

        string LoadFeedDescription(Table table, string? additionalInformation);

        public IFilterbarViewModel FilterbarViewModel { get; }
    }
}
