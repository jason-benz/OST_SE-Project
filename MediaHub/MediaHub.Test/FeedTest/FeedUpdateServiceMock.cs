using MediaHub.Data.FeedModule.Model;

namespace MediaHub.Test.FeedTest
{
    public class FeedUpdateServiceMock : IFeedUpdateService
    {
        public void AddToFeed(string userId, Table table, string? additionalInformation = null)
        {
            // No action required in the mock
        }
    }
}
