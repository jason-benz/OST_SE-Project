using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.PersistencyLayer;
using Microsoft.EntityFrameworkCore;

namespace MediaHub.Data.ContactsModule.Persistency;

public class ContactDataManager : IContactDataManager
{
    public Contact? GetContact(string userId)
    {
        using MediaHubDBContext context = new();
        var contact = context.Contacts
                .FirstOrDefault(c => c.UserId == userId || c.ContactId == userId);
        return contact;
    }

    public List<Contact> GetContacts(string userId)
    {
        using MediaHubDBContext context = new();

        var query = context.Contacts
            .Include(c => c.UserProfile)
            .Include(c => c.ContactUserProfile)
            .Where(c => (c.UserId == userId || c.ContactId == userId) &&
                !c.IsBlocked && !c.OpenRequest);

        return query.ToList();
    }

    public List<string> GetContactIds(string userId)
    {
        using MediaHubDBContext context = new();

        var contactIds = context.Contacts
            .Where(c => c.UserId == userId || c.ContactId == userId)
            .Where(c => !c.IsBlocked && !c.OpenRequest)
            .Select(c => c.UserId == userId ? c.ContactId : c.UserId)
            .ToList();
        return contactIds;
    }
    
    public bool RemoveContact(string userId, string contactId)
    {
        using MediaHubDBContext context = new();
        Contact? contact = context.Contacts
            .FirstOrDefault(c => c.UserId == userId && c.ContactId == contactId ||
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
        var alreadyInDatabase = context.Contacts
            .FirstOrDefault(c => (c.UserId == userId && c.ContactId == contactId) ||
                                    (c.UserId == contactId && c.ContactId == userId));

        if (alreadyInDatabase != null)
        {
            return false;
        }

        if (userId != string.Empty && contactId != string.Empty)
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
            .Where(c => !(c.IsBlocked || c.OpenRequest));
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
            .FirstOrDefault(c => c.UserId == userId && c.ContactId == contactId || 
                        c.UserId == contactId && c.ContactId == userId);

        if (contact == null)
        {
            return false;
        }

        contact.IsBlocked = true;
        context.SaveChanges();
        return true;
    }

    public bool AcceptContactRequest(string userId, string contactId)
    {
        using MediaHubDBContext context = new();
        var contact = context.Contacts
            .FirstOrDefault(c => (c.UserId == userId && c.ContactId == contactId) ||
                        (c.UserId == contactId && c.ContactId == userId) &&
                        (c.OpenRequest));

        if (contact != null)
        {
            contact.OpenRequest = false;
            context.SaveChanges();
            return true;
        }
        return false;
    }

    public List<Contact> GetPendingRequests(string userId)
    {
        using MediaHubDBContext context = new();
        var contacts = context.Contacts
            .Include(c => c.UserProfile)
            .Include(c => c.ContactUserProfile)   
            .Where(c => c.ContactId == userId && c.OpenRequest);
        return contacts.ToList();
    }
}