using MediaHub.Data.FeedModule.Model;

namespace MediaHub.Data.FeedModule.ViewModel
{
    public interface IFeedViewModel
    {
        string UserId { get; set; }

        IEnumerable<FeedItem> FeedItems { get; }
        
        event Action RefreshRequested;

        IFilterbarViewModel FilterbarViewModel { get; }

        string LoadFeedDescription(Table table, string? additionalInformation);

        void LoadFeedItems();
    }
}
