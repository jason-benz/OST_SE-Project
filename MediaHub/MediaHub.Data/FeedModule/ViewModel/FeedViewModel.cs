using MediaHub.Data.FeedModule.Model;

namespace MediaHub.Data.FeedModule.ViewModel
{
    public class FeedViewModel : IFeedViewModel
    {
        private readonly IFeedService _feedService;
        public string UserId { get; set; }
        public IEnumerable<FeedItem> FeedItems { get; private set; }
        
        public IFilterbarViewModel FilterbarViewModel { get; }

        public FeedViewModel(IFeedService feedService)
        {
            _feedService = feedService;

            var filterProperties = new Dictionary<string, bool>()
            {
                {"Media Ratings", true}, 
                {"User profile update", true}, 
            };
            
            FilterbarViewModel = new FilterBarViewModel(filterProperties);
            FilterbarViewModel.OnFilterChanged += (string name, bool value) =>
            {
                LoadFilteredFeedItems(filterProperties);
            };
        }

        public string LoadFeedDescription(Table table, string? additionalInformation)
        {
            if (table == Table.UserProfile)
            {
                return "The user profile has been updated.";
            } 
            else if (table == Table.MediaRating)
            {
                return $"An interaction was made with the movie {additionalInformation}";
            }

            return string.Empty;
        }

        private void LoadFilteredFeedItems(Dictionary<string, bool> filterSettings)
        {
            FeedItems = _feedService.LoadFilteredFeedItems(UserId, filterSettings).OrderByDescending(f => f.CreationDate);
        }

    }
}
