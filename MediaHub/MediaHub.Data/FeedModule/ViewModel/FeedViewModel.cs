using MediaHub.Data.FeedModule.Model;

namespace MediaHub.Data.FeedModule.ViewModel
{
    public class FeedViewModel : IFeedViewModel
    {
        private readonly IFeedDataManager _feedDataManager;

        public FeedViewModel(IFeedDataManager feedDataManager)
        {
            _feedDataManager = feedDataManager;
        }
    }
}
