using System.Collections.Generic;
using System.Linq;
using MediaHub.Data.MessagingModule.Model;
using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Test.ChatTests;

public class ChatDataManagerMock : IChatDataManager
{
    private List<Message> _messages = new();

    public ChatDataManagerMock()
    {
        var message1 = new Message()
        {
            Receiver = new UserProfile("MockId-1"),
            Sender = new UserProfile("MockId-2"),
            Content = "Message1"
        };
        _messages.Add(message1);
        var message2 = new Message()
        {
            Receiver = new UserProfile("MockId-2"),
            Sender = new UserProfile("MockId-1"),
            Content = "Message2"
        };
        _messages.Add(message2);
        var message3 = new Message()
        {
            Receiver = new UserProfile("MockId-5"),
            Sender = new UserProfile("MockId-6"),
            Content = "Message should not appear"
        };
        _messages.Add(message3);
    }

    public void InsertMessage(Message message)
    {
        var message3 = new Message()
        {
            Receiver = new UserProfile("MockId-4"),
            Sender = new UserProfile("MockId-1"),
            Content = "Message3"
        };
        _messages.Add(message3);
    }

    public List<Message> GetMessagesBetweenTwoUsers(string userId1, string userId2)
    {
        return _messages.Where(message => IsReceiverOrSender(userId1, message)
        && IsReceiverOrSender(userId2, message)).ToList();
    }
    
    private static bool IsReceiverOrSender(string userId, Message message)
    {
        return message?.Receiver?.UserId == userId || message?.Sender.UserId == userId;
    }
}