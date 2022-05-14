using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Data.ContactsModule.Model;

public interface IContactDataManager
{
    public Contact GetContact(string userId);
    public List<string> GetContacts(string userId);
    public bool RemoveContact(string userId, string contactId);
    public bool AddContact(string userId, string contactId);

    public bool AreContacts(string userId, string contactId);
    public bool BlockContact(string userId, string contactId);
    public bool acceptContactRequest(string userId, string contactId);
}