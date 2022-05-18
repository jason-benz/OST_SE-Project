using MediaHub.Data.ContactsModule.Model;
using System.Collections.Generic;

namespace MediaHub.Test.ContactTest
{
    public class ContactDataManagerMock : IContactDataManager
    {
        public bool AcceptContactRequest(string userId, string contactId)
        {
            throw new System.NotImplementedException();
        }

        public bool AddContact(string userId, string contactId)
        {
            throw new System.NotImplementedException();
        }

        public bool AreContacts(string userId, string contactId)
        {
            throw new System.NotImplementedException();
        }

        public bool BlockContact(string userId, string contactId)
        {
            throw new System.NotImplementedException();
        }

        public Contact GetContact(string userId)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetContacts(string userId)
        {
            return new List<string> { $"{userId}-Contact" };
        }

        public bool RemoveContact(string userId, string contactId)
        {
            throw new System.NotImplementedException();
        }
    }
}
