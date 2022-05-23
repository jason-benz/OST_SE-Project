using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Data.ContactsModule.Model;

public interface IContactDataManager
{
    public Contact GetContact(string userId);
    public List<Contact> GetContacts(string userId);
    public List<string> GetContactIds(string userId);
    public bool RemoveContact(string userId, string contactId);
    public bool AddContact(string userId, string contactId);

    public bool AreContacts(string userId, string contactId);
    public bool BlockContact(string userId, string contactId);
    public bool AcceptContactRequest(string userId, string contactId);
    public List<Contact> GetPendingRequests(string userId);
}