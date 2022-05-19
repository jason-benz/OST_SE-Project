using System;
using System.Collections.Generic;
using System.Linq;
using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Test.ContactTest;

public sealed class ContactDatabaseFixture : IDisposable
{
    private readonly List<string> MockUserIds;
    
    public ContactDatabaseFixture()
    {
        MockUserIds = MockUser.GetMockUserIds();
        AddUserProfiles();
        AddContacts();
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

    private void AddUserProfiles()
    {
        List<UserProfile> users = new();
        MockUserIds.ForEach(id => users.Add(new UserProfile(id)));

        using MediaHubDBContext context = new();
        context.UserProfiles.AddRange(users);
        context.SaveChanges();
    }

    private void AddContacts()
    {
        List<Contact> contacts = new()
        {
            new Contact(MockUserIds[0], MockUserIds[1]),
            new Contact(MockUserIds[1], MockUserIds[2]),
            new Contact(MockUserIds[2], MockUserIds[3])
        };

        using MediaHubDBContext context = new();
        context.Contacts.AddRange(contacts);
        context.SaveChanges();
    }
}