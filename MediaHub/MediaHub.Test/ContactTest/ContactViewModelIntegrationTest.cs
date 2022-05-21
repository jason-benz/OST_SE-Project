using System;
using System.Collections.Generic;
using System.Linq;
using MediaHub.Data.ContactsModule.Persistency;
using MediaHub.Data.ContactsModule.ViewModel;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using Xunit;

namespace MediaHub.Test.ContactTest;


[Collection("Sequential")]
public class ContactViewModelIntegrationTest : IDisposable
{
    private readonly IContactViewModel _contactViewModel;
    private readonly List<string> MockUserIds = MockUser.GetMockUserIds();

    public ContactViewModelIntegrationTest()
    {
        _contactViewModel = new ContactViewModel(new ContactDataManager());

        List<UserProfile> users = new();
        MockUserIds.ForEach(id => users.Add(new UserProfile(id)));

        using MediaHubDBContext context = new();
        context.UserProfiles.AddRange(users);
        context.SaveChanges();
    }
    
    public void Dispose()
    {
        using MediaHubDBContext context = new();

        var contacts = context.Contacts.Where(c => MockUserIds.Contains(c.UserId));
        context.Contacts.RemoveRange(contacts);

        var users = context.UserProfiles.Where(up => MockUserIds.Contains(up.UserId));
        context.UserProfiles.RemoveRange(users);

        context.SaveChanges();
    }

    [Fact]
    public void AreNotContacts()
    {
        bool areContacts = _contactViewModel.IsContact(MockUserIds[0], MockUserIds[1]);
        Assert.False(areContacts);
    }

    [Fact]
    public void RequestContact()
    {
        bool requestContact = _contactViewModel.AddContact(MockUserIds[0], MockUserIds[1]);
        Assert.True(requestContact);
    }

    [Fact]
    public void AreNotFriends()
    {
        bool areFriends = _contactViewModel.IsContact(MockUserIds[0], MockUserIds[1]);
        Assert.False(areFriends);
    }

    [Fact]
    public void AcceptContactRequest()
    {
        _contactViewModel.AddContact(MockUserIds[1], MockUserIds[2]);
        Assert.True(_contactViewModel.AcceptContactRequest(MockUserIds[1], MockUserIds[2]));
    }

    [Fact]
    public void BlockUser()
    {
        _contactViewModel.BlockContact(MockUserIds[0], MockUserIds[1]);
        Assert.False(_contactViewModel.IsContact(MockUserIds[0], MockUserIds[1]));
    }

    [Fact]
    public void RemoveContact()
    {
        _contactViewModel.AddContact(MockUserIds[0], MockUserIds[1]);
        _contactViewModel.AcceptContactRequest(MockUserIds[0], MockUserIds[1]);
        _contactViewModel.RemoveContact(MockUserIds[1], MockUserIds[0]);
        Assert.False(_contactViewModel.IsContact(MockUserIds[0], MockUserIds[1]));
    }
    
}