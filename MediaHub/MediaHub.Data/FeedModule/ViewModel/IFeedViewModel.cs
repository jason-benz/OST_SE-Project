using MediaHub.Data.FeedModule.Model;

namespace MediaHub.Data.FeedModule.ViewModel
{
    public interface IFeedViewModel
    {
        public IEnumerable<FeedItem> FeedItems { get; }
        public string UserId { get; set; }
        void LoadAllFeedItems();

        void LoadFilteredFeedItems(Dictionary<string, bool> filterSettings);

        string LoadFeedDescription(Table table, string? additionalInformation);

        public IFilterbarViewModel FilterbarViewModel { get; }
    }
}
