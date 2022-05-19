namespace MediaHub.Data.ContactsModule.ViewModel
{
    public interface IContactViewModel
    {
        List<string> GetContacts(string userId);

        bool AddContact(string userId, string contactId);

        bool AcceptContactRequest(string userId, string contactId);

        bool RemoveContact(string userId, string contactId);

        bool BlockContact(string userId, string contactId);

        bool IsContact(string userId, string contactId);
    }
}
