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
        using MediaHubDBContext context = new();
        var contact = context.Contacts
                .First(c => c.UserId == userId);
        return contact;
    }

    public List<string> GetContacts(string userId)
    {
        using MediaHubDBContext context = new();
        
        var contacts = context.Contacts
            .Where(c => c.UserId == userId || c.ContactId == userId)
            .Where(c => c.IsBlocked == false && c.OpenRequest == false)
            .Select(c => c.ContactId)
            .ToList();
        return contacts;
    }
    
    public bool RemoveContact(string userId, string contactId)
    {
        using MediaHubDBContext context = new();
        Contact? contact = context.Contacts
            .First(c => c.UserId == userId && c.ContactId == contactId ||
                         c.UserId == contactId && c.ContactId == userId);

        if (contact == null)
        {
            return false;
        }
        context.Remove(contact);
        context.SaveChanges();
        return true;
    }

    public bool AddContact(string userId, string contactId)
    {
        using MediaHubDBContext context = new();
        var alreadyInDatabase = context.Contacts.Where(c => c.UserId == userId && c.ContactId == contactId ||
                                    c.UserId == contactId && c.ContactId == userId);

        if (alreadyInDatabase.Any())
        {
            return false;
        }

        if (userId != String.Empty && contactId != String.Empty)
        {
            Contact contact = new Contact(userId, contactId);
            contact.OpenRequest = true;
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
            .Where(c => c.UserId == userId && c.ContactId == contactId ||
                c.UserId == contactId && c.ContactId == userId)
            .Where(c => c.IsBlocked == false && c.OpenRequest == false);
        if (contact.Any())
        {
            return true;
        }
        return false;
    }
    
    public bool BlockContact(string userId, string contactId)
    {
        using MediaHubDBContext context = new();
        var contact = context.Contacts
            .First(c => c.UserId == userId && c.ContactId == contactId || 
                        c.UserId == contactId && c.ContactId == userId);

        if (contact == null)
        {
            return false;
        }

        contact.IsBlocked = true;
        return true;
    }

    public bool AcceptContactRequest(string userId, string contactId)
    {
        using MediaHubDBContext context = new();
        var contact = context.Contacts
            .First(c => (c.UserId == userId && c.ContactId == contactId) ||
                        (c.UserId == contactId && c.ContactId == userId) &&
                        (c.OpenRequest == true));

        if (contact != null)
        {
            contact.OpenRequest = false;
            context.SaveChanges();
            return true;
        }
        return false;
    }
}