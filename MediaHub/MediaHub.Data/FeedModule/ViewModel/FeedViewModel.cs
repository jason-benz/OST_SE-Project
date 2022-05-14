using MediaHub.Data.FeedModule.Model;

namespace MediaHub.Data.FeedModule.ViewModel
{
    public class FeedViewModel : IFeedViewModel
    {
        private readonly IFeedService _feedService;

        public FeedViewModel(IFeedService feedService)
        {
            _feedService = feedService;

            var filterProperties = new Dictionary<string, bool>()
            {
                {"Property 1 filter xy set by default", true},
                {"Property 2 filter abc", false},
            };
            FilterbarViewModel = new FilterBarViewModel(filterProperties);
            FilterbarViewModel.OnFilterChanged += (string name, bool value) => {Console.WriteLine($"Filter {name} has been set to {value}");};
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

        public IEnumerable<FeedItem> LoadAllFeedItems(string userId)
        {
            return _feedService.LoadAllFeedItems(userId);
        }

        public IEnumerable<FeedItem> LoadFilteredFeedItems(string userId, Dictionary<string, bool> filterSettings)
        {
            return _feedService.LoadFilteredFeedItems(userId, filterSettings);
        }

        public IFilterbarViewModel FilterbarViewModel { get; }
    }
}
