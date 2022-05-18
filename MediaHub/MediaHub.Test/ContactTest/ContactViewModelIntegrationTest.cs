using System.Collections.Generic;
using MediaHub.Data.ContactsModule.Persistency;
using MediaHub.Data.ContactsModule.ViewModel;
using Xunit;

namespace MediaHub.Test.ContactTest;


[Collection("Sequential")]
public class ContactViewModelIntegrationTest
{
    private readonly ContactViewModel _contactViewModel;

    private readonly List<string> MockUsers = new List<string>()
    {
        "c67f490b-e0a0-460d-95b0-6505910f6700",
        "c87b8391-0e96-45d8-a3cf-9300498f5a701",
        "9d6855fb-7aff-4a55-b41e-aab429a54d702",
        "9d6855fb-7aff-4a55-b41e-aab429adsd703"
    };
    
    public ContactViewModelIntegrationTest()
    {
        _contactViewModel = new ContactViewModel(new ContactDataManager());
    }

    [Fact]
    public void AreNotContacts()
    {
        bool areContacts = _contactViewModel.IsContact(MockUsers[0], MockUsers[1]);
        Assert.False(areContacts);
    }

    [Fact]
    public void RequestContact()
    {
        bool requestContact = _contactViewModel.AddContact(MockUsers[0], MockUsers[1]);
        Assert.True(requestContact);
    }

    [Fact]
    public void AreNotFriends()
    {
        bool areFriends = _contactViewModel.IsContact(MockUsers[0], MockUsers[1]);
        Assert.False(areFriends);
    }

    [Fact]
    public void AcceptContactRequest()
    {
        _contactViewModel.AddContact(MockUsers[1], MockUsers[2]);
        Assert.True(_contactViewModel.AcceptContactRequest(MockUsers[1], MockUsers[2]));
    }

    [Fact]
    public void BlockUser()
    {
        _contactViewModel.BlockContact(MockUsers[0], MockUsers[1]);
        Assert.False(_contactViewModel.IsContact(MockUsers[0], MockUsers[1]));
    }

    [Fact]
    public void RemoveContact()
    {
        _contactViewModel.AddContact(MockUsers[0], MockUsers[1]);
        _contactViewModel.AcceptContactRequest(MockUsers[0], MockUsers[1]);
        _contactViewModel.RemoveContact(MockUsers[1], MockUsers[0]);
        Assert.False(_contactViewModel.IsContact(MockUsers[0], MockUsers[1]));
    }
    
}