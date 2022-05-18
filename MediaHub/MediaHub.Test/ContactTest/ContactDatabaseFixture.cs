using System;
using System.Collections.Generic;
using System.Linq;
using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.PersistencyLayer;

namespace MediaHub.Test.ContactTest;

public sealed class ContactDatabaseFixture : IDisposable
{
    private readonly List<string> MockUsers;
    
    public ContactDatabaseFixture()
    {
        MockUsers = MockUser.GetMockUsers();
        var contact1 = new Contact(MockUsers[0], MockUsers[1]);
        var contact2 = new Contact(MockUsers[1], MockUsers[2]);
        var contact3 = new Contact(MockUsers[2], MockUsers[3]);

        using MediaHubDBContext context = new();
        context.Contacts.Add(contact1);
        context.Contacts.Add(contact2);
        context.Contacts.Add(contact3);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using MediaHubDBContext context = new();
        var contact1 = context.Contacts.First(c => c.UserId == MockUsers[1]);
        var contact2 = context.Contacts.First(c => c.UserId == MockUsers[2]);
        var contact3 = context.Contacts.First(c => c.UserId == MockUsers[3]);

        context.Contacts.Remove(contact1);
        context.Contacts.Remove(contact2);
        context.Contacts.Remove(contact3);
        context.SaveChanges();
    }
}