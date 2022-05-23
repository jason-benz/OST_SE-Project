using MediaHub.Data.ContactsModule.Model;

namespace MediaHub.Data.ContactsModule.ViewModel
{
    public interface IContactViewModel
    {
        List<Contact> GetContacts(string userId);
        
        List<string> GetContactIds(string userId);

        bool AddContact(string userId, string contactId);

        bool AcceptContactRequest(string userId, string contactId);

        List<Contact> GetPendingRequests(string userId);

        bool RemoveContact(string userId, string contactId);

        bool BlockContact(string userId, string contactId);

        bool IsContact(string userId, string contactId);
    }
}
