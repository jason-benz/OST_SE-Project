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
            Receiver = new UserProfile("Mock-Id1"),
            Sender = new UserProfile("Mock-Id2"),
            Content = "Message1"
        };
        _messages.Add(message1);
        var message2 = new Message()
        {
            Receiver = new UserProfile("Mock-Id2"),
            Sender = new UserProfile("Mock-Id1"),
            Content = "Message2"
        };
        _messages.Add(message2);
        var message3 = new Message()
        {
            Receiver = new UserProfile("Mock-Id5"),
            Sender = new UserProfile("Mock-Id6"),
            Content = "Message should not appear"
        };
        _messages.Add(message3);
    }

    public void InsertMessage(Message message)
    {
        var message3 = new Message()
        {
            Receiver = new UserProfile("Mock-Id4"),
            Sender = new UserProfile("Mock-Id1"),
            Content = "Message3"
        };
        _messages.Add(message3);
    }

    public List<Message> GetMessagesBetweenTwoUsers(string userId1, string userId2)
    {
        return _messages;
    }
    
    private static bool IsReceiverOrSender(string userId, Message message)
    {
        return message?.Receiver?.UserId == userId || message?.Sender.UserId == userId;
    }
}