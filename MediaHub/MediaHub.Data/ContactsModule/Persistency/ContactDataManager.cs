using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.Migrations;
using MediaHub.Data.PersistencyLayer;
using Microsoft.EntityFrameworkCore;
using UserProfile = MediaHub.Data.ProfileModule.Model.UserProfile;

namespace MediaHub.Data.ContactsModule.Persistency;

public class ContactDataManager : IContactDataManager
{
    public Contact GetContact(string userId)
    {
        using (var context = new MediaHubDBContext())
        {
            var contact = context.Contacts
                .First(c => c.UserId == userId);
            return contact;
        }
    }

    public List<string> GetContacts(string userId)
    {
        using (var context = new MediaHubDBContext())
        {
            var contacts = context.Contacts
                .Where(c => c.UserId == userId)
                .Select(c => c.ContactId)
                .ToList();
            return contacts;
        }
    }
    
    public bool RemoveContact(string userId, string contactId)
    {
        using MediaHubDBContext context = new();
        var contacts = context.Contacts
            .Where(c => c.UserId == userId && c.ContactId == contactId)
            .ToList();
        
        if (contacts.Count == 0)
        {
            return false; 
        }
        foreach (var c in contacts)
        {
            context.Remove(c);
        }

        context.SaveChanges();
        return true;
    }

    public bool AddContact(string userId, string contactId)
    {
        using MediaHubDBContext context = new();
        if (userId != String.Empty && contactId != String.Empty)
        {
            Contact contact = new Contact(userId, contactId);
            contact.openRequest = true;
            context.Add(contact);
            context.SaveChanges();
            return true;
        }

        return false;
    }

    public bool AreContacts(string userId, string contactId)
    {
        using MediaHubDBContext context = new();
        var contact = context.Contacts
            .Where(c => c.UserId == userId || c.ContactId == userId)
            .Where(c => c.ContactId == contactId || c.UserId == contactId)
            .Where(c => c.isBlocked == false && c.openRequest == false);
        if (contact != null)
        {
            return false;
        }
        return true;
    }
    
    public bool BlockContact(string userId, string contactId)
    {
        using MediaHubDBContext context = new();
        var contact = context.Contacts
            .First(c => c.UserId == userId && c.ContactId == contactId);

        if (contact == null)
        {
            return false;
        }

        contact.isBlocked = true;
        return true;
    }

    public bool acceptContactRequest(string userId, string contactId)
    {
        using MediaHubDBContext context = new();
        Contact contact = context.Contacts
            .First(c => c.UserId == userId && c.ContactId == contactId ||
                        c.UserId == contactId && c.ContactId == userId);

        if (contact != null)
        {
            contact.openRequest = false;
            context.SaveChanges();
            return true;
        }
        return false;
    }
}