using MediaHub.Data.ContactsModule.Model;
using System;
using System.Collections.Generic;

namespace MediaHub.Test.ContactTest
{
    public class ContactDataManagerMock : IContactDataManager
    {
        public bool AcceptContactRequest(string userId, string contactId)
        {
            if (userId == "MockId-1" && contactId == "MockId-1-Contact")
            {
                return true;
            }

            return false;
        }

        public List<Contact> GetPendingRequests(string userId)
        {
            throw new NotImplementedException();
        }

        public bool AddContact(string userId, string contactId)
        {
            if (userId == "MockId-2" && contactId == "MockId-2-Contact")
            {
                return true;
            }
            else if (userId == "MockId-3" && contactId == "MockId-3-Contact")
            {
                throw new Exception();
            }
            
            return false;
        }

        public bool AreContacts(string userId, string contactId)
        {
            if (userId == "MockId-1" && contactId == "MockId-1-Contact")
            {
                return true;
            }
            
            return false;
        }

        public bool BlockContact(string userId, string contactId)
        {
            if (userId == "MockId-1" && contactId == "MockId-1-Contact")
            {
                return true;
            }

            return false;
        }

        public Contact GetContact(string userId)
        {
            throw new System.NotImplementedException();
        }

        public List<Contact> GetContacts(string userId)
        {
            throw new NotImplementedException();
        }


        public List<string> GetContactIds(string userId)
        {
            switch (userId)
            {
                case "MockId-1":
                    return new List<string> { $"{userId}-Contact" };
                case "MockId-2":
                    return new List<string> 
                    { 
                        $"{userId}-Contact-1",
                        $"{userId}-Contact-2"
                    };
                case "MockId-3":
                    return new List<string>();
                default:
                    throw new NotImplementedException();
            }
        }

        public List<Contact> GetContacts(string userId, bool includeAllUsers)
        {
            throw new NotImplementedException();
        }

        public bool RemoveContact(string userId, string contactId)
        {
            if (userId == "MockId-1" && contactId == "MockId-1-Contact")
            {
                return true;
            }

            return false;
        }
    }
}
