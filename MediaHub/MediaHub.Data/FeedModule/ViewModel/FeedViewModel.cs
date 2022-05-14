using MediaHub.Data.FeedModule.Model;

namespace MediaHub.Data.FeedModule.ViewModel
{
    public class FeedViewModel : IFeedViewModel
    {
        private readonly IFeedDataManager _feedDataManager;

        public FeedViewModel(IFeedDataManager feedDataManager)
        {
            _feedDataManager = feedDataManager;
            var filterProperties = new Dictionary<string, bool>()
            {
                {"Property 1 filter xy set by default", true},
                {"Property 2 filter abc", false},
            };
            FilterbarViewModel = new FilterBarViewModel(filterProperties);
            FilterbarViewModel.OnFilterChanged += (string name, bool value) => {Console.WriteLine($"Filter {name} has been set to {value}");};
        }
        
        public IFilterbarViewModel FilterbarViewModel { get; }
    }
}
