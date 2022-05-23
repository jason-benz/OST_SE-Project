using System;
using System.Collections.Generic;
using System.Linq;
using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.ContactsModule.Persistency;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using Xunit;

namespace MediaHub.Test.ContactTest;
[Collection("Sequential")]
public class ContactDataManagerIntegrationTest : IClassFixture<ContactDatabaseFixture>
{
    private readonly IContactDataManager _contactDataManager = new ContactDataManager();
    private readonly ContactDatabaseFixture _contactDatabaseFixture;
    private readonly List<string> MockUserIds;

    public ContactDataManagerIntegrationTest(ContactDatabaseFixture contactDatabaseFixture)
    {
        MockUserIds = MockUser.GetMockUserIds();
        _contactDatabaseFixture = contactDatabaseFixture;
    }

    [Fact]
    public void GetContactTest()
    {
        var contact = _contactDataManager.GetContact(MockUserIds[3]);
        Assert.Equal(MockUserIds[2], contact.UserId);
    }

    [Fact]
    public void GetContactsId()
    {
        var contacts = _contactDataManager.GetContactIds(MockUserIds[0]);
        Assert.All(contacts, contact => Assert.Contains(MockUserIds[1], contact));
    }

    [Fact]
    public void AddContact()
    {
        bool addedContact = _contactDataManager.AddContact(MockUserIds[3], MockUserIds[1]);
        using MediaHubDBContext context = new();
        Contact contact = context.Contacts
            .First(c => c.UserId == MockUserIds[3] && c.ContactId == MockUserIds[1]);

        Assert.True(addedContact && contact != null);
    }
    
    [Fact]
    public void BlockContact()
    {
        _contactDataManager.BlockContact(MockUserIds[0], MockUserIds[1]);

        using MediaHubDBContext context = new();
        var contact = context.Contacts
            .First(c => c.UserId == MockUserIds[0]);
        contact.IsBlocked = true;
        context.SaveChanges();
        
        Assert.True(contact.IsBlocked);
    }

    [Fact]
    public void AreFriends()
    {
        _contactDataManager.AddContact(MockUserIds[3], MockUserIds[1]);
        _contactDataManager.AcceptContactRequest(MockUserIds[1], MockUserIds[3]);
        bool areContacts = _contactDataManager.AreContacts(MockUserIds[3], MockUserIds[1]);
        Assert.True(areContacts);
    }
    
    [Fact]
    public void AreNoFriends()
    {
        bool areNoContacts = _contactDataManager.AreContacts(MockUserIds[0], MockUserIds[3]);
        Assert.False(areNoContacts);
    }
    
    [Fact]
    public void RemoveContact()
    {
        var contact = MockUserIds[1];
        var contactRemoved = _contactDataManager.RemoveContact(MockUserIds[0], MockUserIds[1]);
        Assert.True(contactRemoved);
    }
}