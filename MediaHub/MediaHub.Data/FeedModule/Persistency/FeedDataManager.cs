using MediaHub.Data.FeedModule.Model;
using MediaHub.Data.PersistencyLayer;
using Microsoft.EntityFrameworkCore;

namespace MediaHub.Data.FeedModule.Persistency
{
    public class FeedDataManager : IFeedDataManager
    {
        public void AddFeedItem(FeedItem feedItem)
        {
             using MediaHubDBContext context = new();
             context.Add(feedItem);
             context.SaveChanges();
        }

        public bool IsItemExisting(string userId, Table table, string? additionalInformation)
        {
            using MediaHubDBContext context = new();
            return context.FeedItems
                .Where(f => f.UserId == userId && 
                            f.ChangedTable == table && 
                            f.AdditionalInformation == additionalInformation &&
                            f.CreationDate.Day == DateTime.Now.Day)
                .Any();
        }

        public IEnumerable<FeedItem> LoadAllFeedItems(IEnumerable<string> userIds)
        {
            using MediaHubDBContext context = new();
            return context.FeedItems
                .Include(f => f.UserProfile)
                .Where(f => userIds.Contains(f.UserId))
                .ToList();
        }

        public IEnumerable<FeedItem> LoadFilteredFeedItems(IEnumerable<string> userIds, IEnumerable<Table> selectedTables)
        {
            using MediaHubDBContext context = new();
            return context.FeedItems
                .Include(f => f.UserProfile)
                .Where(f => userIds.Contains(f.UserId) && selectedTables.Contains(f.ChangedTable))
                .ToList();
        }
    }
}
