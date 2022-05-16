using MediaHub.Data.FeedModule.Model;

namespace MediaHub.Data.FeedModule.ViewModel
{
    public interface IFeedViewModel
    {
        string UserId { get; set; }

        IEnumerable<FeedItem> FeedItems { get; }
        
        string LoadFeedDescription(Table table, string? additionalInformation);

        IFilterbarViewModel FilterbarViewModel { get; }
    }
}
