using System;
using System.Collections.Generic;
using System.Linq;
using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.ContactsModule.Persistency;
using MediaHub.Data.PersistencyLayer;
using Xunit;

namespace MediaHub.Test.ContactTest;
[Collection("Sequential")]
public class ContactDataManagerIntegrationTest : IDisposable
{
    private readonly IContactDataManager _contactDataManager = new ContactDataManager();

    private readonly List<string> MockUsers = new List<string>()
    {
        "c67f490b-e0a0-460d-95b0-6505910f6600",
        "c87b8391-0e96-45d8-a3cf-9300498f5601",
        "9d6855fb-7aff-4a55-b41e-aab429a54602",
        "9d6855fb-7aff-4a55-b41e-aab429a70603"
    };

    public ContactDataManagerIntegrationTest()
    {
        var contact1 = new Contact(MockUsers[0], MockUsers[1]);
        var contact2 = new Contact(MockUsers[1], MockUsers[2]);
        var contact3 = new Contact(MockUsers[2], MockUsers[3]);

        using MediaHubDBContext context = new();
        context.Add(contact1);
        context.Add(contact2);
        context.Add(contact3);
        context.SaveChanges();
    }

    public void Dispose()
    {
        _contactDataManager.RemoveContact(MockUsers[0], MockUsers[1]);
        _contactDataManager.RemoveContact(MockUsers[1], MockUsers[2]);
        _contactDataManager.RemoveContact(MockUsers[2], MockUsers[3]);
        _contactDataManager.RemoveContact(MockUsers[1], MockUsers[3]);
    }

    [Fact]
    public void GetContact()
    {
        var contact = _contactDataManager.GetContact(MockUsers[3]);
        Assert.Equal(MockUsers[2], contact.UserId);
    }

    [Fact]
    public void GetContacts()
    {
        var contacts = _contactDataManager.GetContacts(MockUsers[0]);
        Assert.All(contacts, contact => Assert.Contains(MockUsers[1], contact));
    }

    [Fact]
    public void AddContact()
    {
        bool addedContact = _contactDataManager.AddContact(MockUsers[3], MockUsers[1]);
        using MediaHubDBContext context = new();
        Contact contact = context.Contacts
            .First(c => c.UserId == MockUsers[3] && c.ContactId == MockUsers[1]);

        Assert.True(addedContact && contact != null);
    }
    
    [Fact]
    public void BlockContact()
    {
        _contactDataManager.BlockContact(MockUsers[0], MockUsers[1]);

        using MediaHubDBContext context = new();
        var contact = context.Contacts
            .First(c => c.UserId == MockUsers[0]);
        contact.IsBlocked = true;
        context.SaveChanges();
        
        Assert.True(contact.IsBlocked);
    }

    [Fact]
    public void AreFriends()
    {
        _contactDataManager.AddContact(MockUsers[3], MockUsers[1]);
        _contactDataManager.AcceptContactRequest(MockUsers[1], MockUsers[3]);
        bool areContacts = _contactDataManager.AreContacts(MockUsers[3], MockUsers[1]);
        Assert.True(areContacts);
    }
    
    [Fact]
    public void AreNoFriends()
    {
        bool areNoContacts = _contactDataManager.AreContacts(MockUsers[0], MockUsers[3]);
        Assert.False(areNoContacts);
    }
    
    [Fact]
    public void RemoveContact()
    {
        var contact = MockUsers[1];
        var contactRemoved = _contactDataManager.RemoveContact(MockUsers[0], MockUsers[1]);
        Assert.True(contactRemoved);
    }
}