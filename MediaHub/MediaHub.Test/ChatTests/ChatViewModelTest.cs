using System;
using System.Linq;
using MediaHub.Data.MessagingModule.Model;
using MediaHub.Data.MessagingModule.ViewModel;
using MediaHub.Pages;
using MediaHub.Test.ContactTest;
using MediaHub.Test.UserProfileTest;
using Microsoft.CodeAnalysis.Diagnostics;
using Xunit;

namespace MediaHub.Test.ChatTests;

public class ChatViewModelTest
{
    private readonly ChatViewModel _chatViewModel = new (new ChatDataManagerMock(),
        new UserProfileDataManagerMock(), new ContactDataManagerMock());

    public ChatViewModelTest()
    {
        _chatViewModel.SetUserById("MockId-1");
    }

    [Fact, Trait("Category", "Unit")]
    public void SetUserId()
    {
        Assert.True(_chatViewModel.User?.UserId == "MockId-1");
    }

    [Fact, Trait("Category", "Unit")]
    public void LoadAllContactUserProfiles()
    {
        var userId = "MockId-1";
        var expectedContactId = $"{userId}-Contact";

        _chatViewModel.LoadAllContactUserProfiles(userId);

        Assert.Single(_chatViewModel.ContactList);
        Assert.Equal(expectedContactId, _chatViewModel.ContactList.First().UserId);
    }

    [Fact, Trait("Category", "Unit")]
    public void LoadAllContactUserProfiles_Empty()
    {
        var userId = "MockId-3";

        _chatViewModel.LoadAllContactUserProfiles(userId);

        Assert.Empty(_chatViewModel.ContactList);
    }

    [Fact, Trait("Category", "Unit")]
    public void OpenChatContactId()
    {
        _chatViewModel.OpenChat("MockId-4");
        Assert.True(_chatViewModel.Contact?.UserId == "MockId-4");
    }

    [Fact, Trait("Category", "Unit")]
    public void OpenChatActiveMessages()
    {
        _chatViewModel.OpenChat("MockId-4");
        foreach (var message in _chatViewModel.Messages)
        {
            Assert.True(IsReceiverOrSender("MockId-1", message) && 
                        IsReceiverOrSender("MockId-4", message));
        } 
    }

    [Fact, Trait("Category", "Unit")]
    public void SendMessage()
    {
        _chatViewModel.OpenChat("MockId-4");
        _chatViewModel.CurrentMessage = "Message3";
        _chatViewModel.SendMessage();

        var newMessages = _chatViewModel.Messages.Where(m => m.Content == "Message3");
        Assert.Single(newMessages);
    }

    [Fact, Trait("Category", "Unit")]
    public void SendMessage_MessageNull()
    {
        var exception = Record.Exception(() => _chatViewModel.SendMessage());
        Assert.Null(exception);
    }

    [Fact, Trait("Category", "Unit")]
    public void SendMessage_UserNull()
    {
        _chatViewModel.OpenChat("MockId-1");
        var exception = Record.Exception(() => _chatViewModel.SendMessage());
        Assert.Null(exception);
    }

    [Fact, Trait("Category", "Unit")]
    public void SendMessage_AdditionalInvoke()
    {
        _chatViewModel.RefreshRequested += () => Console.WriteLine("Test");
        _chatViewModel.OpenChat("MockId-4");
        _chatViewModel.CurrentMessage = "Message3";
        _chatViewModel.SendMessage();

        var newMessages = _chatViewModel.Messages.Where(m => m.Content == "Message3");
        Assert.Single(newMessages);
    }

    private static bool IsReceiverOrSender(string userId, Message message)
    {
        return message?.Receiver?.UserId == userId || message?.Sender.UserId == userId;
    }
}