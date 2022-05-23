using MediaHub.Data.MessagingModule.Model;
using MediaHub.Data.MessagingModule.Persistency;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using System;
using System.Linq;
using Xunit;

namespace MediaHub.Test.ChatTests
{
    public class ChatDataManagerIntegrationTest
    {
        private readonly IChatDataManager _chatDataManager = new ChatDataManager();

        [Fact]
        public void GetMessagesBetweenTwoUsers_Empty()
        {
            var userId1 = Guid.NewGuid().ToString();
            var userId2 = Guid.NewGuid().ToString();

            var actualMessages = _chatDataManager.GetMessagesBetweenTwoUsers(userId1, userId2);

            Assert.Empty(actualMessages);
        }

        [Fact]
        public void GetMessagesBetweenTwoUsers()
        {
            var expectedMessage = CreateMessage();
            _chatDataManager.InsertMessage(expectedMessage);

            var actualMessages = _chatDataManager.GetMessagesBetweenTwoUsers(expectedMessage.Sender.UserId, expectedMessage.Receiver.UserId);

            Assert.Single(actualMessages);
            Assert.Equal(expectedMessage.MessageId, actualMessages.First().MessageId);
        }

        [Fact]
        public void InsertMessage()
        {
            var expectedMessage = CreateMessage();

            _chatDataManager.InsertMessage(expectedMessage);
            
            var actualMessages = _chatDataManager.GetMessagesBetweenTwoUsers(expectedMessage.Sender.UserId, expectedMessage.Receiver.UserId);
            RemoveTestMessageFromDB(expectedMessage);
            Assert.Single(actualMessages);
            Assert.Equal(expectedMessage.MessageId, actualMessages.First().MessageId);
        }

        private Message CreateMessage()
        {
            var userProfile1 = new UserProfile(Guid.NewGuid().ToString());
            var userProfile2 = new UserProfile(Guid.NewGuid().ToString());

            using MediaHubDBContext context = new();
            context.Add(userProfile1);
            context.Add(userProfile2);
            context.SaveChanges();

            var message = new Message()
            {
                Content = "ChatDataManagerIntegrationTest",
                Sender = userProfile1,
                Receiver = userProfile2
            };

            return message;
        }

        private void RemoveTestMessageFromDB(Message message)
        {
            using MediaHubDBContext context = new();
            context.Messages.Remove(message);
            context.UserProfiles.Remove(message.Sender);
            context.UserProfiles.Remove(message.Receiver);
            context.SaveChanges();
        }
    }
}
