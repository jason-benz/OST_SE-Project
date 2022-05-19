using MediaHub.Data.ContactsModule.Model;

namespace MediaHub.Data.ContactsModule.ViewModel;

public class ContactViewModel : IContactViewModel
{
    private readonly IContactDataManager _contactDataManager;

    public List<Contact> Contacts { get; set; }

    public ContactViewModel(IContactDataManager contactDataManager)
    {
        _contactDataManager = contactDataManager;
    }

    public List<Contact> GetContacts(string userId)
    {
        Contacts = _contactDataManager.GetContacts(userId, true);
        return Contacts;
    }

    public List<string> GetContactIds(string userId)
    {
        return _contactDataManager.GetContactIds(userId);
    }

    public bool AddContact(string userId, string contactId)
    {
        if (!_contactDataManager.AreContacts(userId, contactId))
        {
            _contactDataManager.AddContact(userId, contactId);
            return true;
        }
        return false; 
    }

    public bool AcceptContactRequest(string userId, string contactId)
    {
        return _contactDataManager.AcceptContactRequest(userId, contactId);
    }

    public bool RemoveContact(string userId, string contactId)
    {
        if (_contactDataManager.AreContacts(userId, contactId))
        {
            _contactDataManager.RemoveContact(userId, contactId);
            return true;
        }
        return false;
    }

    public bool BlockContact(string userId, string contactId)
    {
        if (_contactDataManager.AreContacts(userId, contactId))
        {
            _contactDataManager.BlockContact(userId, contactId);
            return true;
        }
        return false;
    }

    public bool IsContact(string userId, string contactId)
    {
        return _contactDataManager.AreContacts(userId, contactId);
    }
}