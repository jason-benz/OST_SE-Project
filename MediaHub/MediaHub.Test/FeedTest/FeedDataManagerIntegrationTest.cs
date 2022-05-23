using MediaHub.Data.FeedModule.Model;
using MediaHub.Data.FeedModule.Persistency;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MediaHub.Test.FeedTest
{
    public class FeedDataManagerIntegrationTest
    {
        private readonly IFeedDataManager _feedDataManager = new FeedDataManager();

        [Fact]
        public void AddFeedItem()
        {
            var expectedFeedItem = CreateFeedItem();
            var userIds = new List<string>() { expectedFeedItem.UserId };
            var changedTables = new List<Table>() { expectedFeedItem.ChangedTable };

            _feedDataManager.AddFeedItem(expectedFeedItem);
            
            var actualFeedItem = _feedDataManager.LoadFilteredFeedItems(userIds, changedTables).First();
            RemoveTestFeedItemFromDB(expectedFeedItem);
            Assert.Equal(expectedFeedItem, actualFeedItem);
        }

        [Fact]
        public void IsItemExisting_True()
        {
            var expectedFeedItem = CreateFeedItem();
            AddFeedItemToDB(expectedFeedItem);

            var isItemExisting = _feedDataManager.IsItemExisting(expectedFeedItem.UserId, expectedFeedItem.ChangedTable, expectedFeedItem.AdditionalInformation);
            RemoveTestFeedItemFromDB(expectedFeedItem);

            Assert.True(isItemExisting);
        }

        [Fact]
        public void IsItemExisting_False()
        {
            var expectedFeedItem = CreateFeedItem();

            var isItemExisting = _feedDataManager.IsItemExisting(expectedFeedItem.UserId, expectedFeedItem.ChangedTable, expectedFeedItem.AdditionalInformation);
            
            Assert.False(isItemExisting);
        }

        [Fact]
        public void LoadFilteredFeedItems_Empty()
        {
            var userIds = new List<string>() { "Test123" };
            var changedTables = new List<Table>() { Table.Feed };
            var actualFeedItems = _feedDataManager.LoadFilteredFeedItems(userIds, changedTables);

            Assert.Empty(actualFeedItems);
        }

        [Fact]
        public void LoadFilteredFeedItems()
        {
            var expectedFeedItem = CreateFeedItem();
            AddFeedItemToDB(expectedFeedItem);
            var userIds = new List<string>() { expectedFeedItem.UserId };
            var changedTables = new List<Table>() { expectedFeedItem.ChangedTable };

            var actualFeedItem = _feedDataManager.LoadFilteredFeedItems(userIds, changedTables).First();
            RemoveTestFeedItemFromDB(expectedFeedItem);

            Assert.Equal(expectedFeedItem, actualFeedItem);
        }

        private FeedItem CreateFeedItem(Table table = Table.Feed)
        {
            var userId = Guid.NewGuid().ToString();
            var userProfile = new UserProfile(userId);

            var feedItem = new FeedItem(userId)
            {
                ChangedTable = table,
                UserProfile = userProfile,
                AdditionalInformation = "FeedDataManagerIntegrationTest"
            };

            return feedItem;
        }

        private void AddFeedItemToDB(FeedItem feedItem)
        {
            using MediaHubDBContext context = new();
            context.Add(feedItem);
            context.SaveChanges();
        }

        private void RemoveTestFeedItemFromDB(FeedItem feedItem)
        {
            using MediaHubDBContext context = new();
            context.FeedItems.Remove(feedItem);
            context.UserProfiles.Remove(feedItem.UserProfile);
            context.SaveChanges();
        }
    }
}
