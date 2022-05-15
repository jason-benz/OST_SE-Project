using MediaHub.Data.ContactsModule.Model;

namespace MediaHub.Data.ContactsModule.ViewModel;

public class ContactViewModel
{
    private readonly IContactDataManager _contactDataManager;
    public ContactViewModel(IContactDataManager contactDataManager)
    {
        _contactDataManager = contactDataManager;
    }

    public List<string> GetContacts(string userId)
    {
        return _contactDataManager.GetContacts(userId);
    }

    public bool RequestContact(string userId, string contactId)
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