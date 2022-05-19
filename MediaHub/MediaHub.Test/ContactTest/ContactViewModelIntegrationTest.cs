using System;
using System.Collections.Generic;
using MediaHub.Data.ContactsModule.Persistency;
using MediaHub.Data.ContactsModule.ViewModel;
using Xunit;

namespace MediaHub.Test.ContactTest;


[Collection("Sequential")]
public class ContactViewModelIntegrationTest : IDisposable
{
    private readonly IContactViewModel _contactViewModel;
    private readonly List<string> MockUsers = MockUser.GetMockUsers();

    public ContactViewModelIntegrationTest()
    {
        _contactViewModel = new ContactViewModel(new ContactDataManager());
    }
    
    public void Dispose()
    {
        _contactViewModel.RemoveContact(MockUsers[1], MockUsers[2]);
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