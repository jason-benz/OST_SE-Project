using MediaHub.Data.ContactsModule.ViewModel;
using System.Linq;
using Xunit;

namespace MediaHub.Test.ContactTest
{
    public class ContactViewModelTest
    {
        private readonly IContactViewModel _contactViewModel = new ContactViewModel(new ContactDataManagerMock());

        [Fact, Trait("Category", "Unit")]
        public void GetContactIds_SingleResults()
        {
            var userId = "MockId-1";
            var contactId = "MockId-1-Contact";

            var contacts = _contactViewModel.GetContactIds(userId);

            Assert.True(contacts.Any());
            Assert.Equal(contacts.First(), contactId);
        }

        [Fact, Trait("Category", "Unit")]
        public void GetContactIds_MultipleResults()
        {
            var userId = "MockId-2";
            var contactId1 = "MockId-2-Contact-1";
            var contactId2 = "MockId-2-Contact-2";

            var contacts = _contactViewModel.GetContactIds(userId);

            Assert.True(contacts.Any());
            Assert.Equal(contacts[0], contactId1);
            Assert.Equal(contacts[1], contactId2);
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("MockId-1", "MockId-1-Contact", false)]
        [InlineData("MockId-2", "MockId-2-Contact", true)]
        public void AddContacts(string userId, string contactId, bool expectedResult)
        {
            var result = _contactViewModel.AddContact(userId, contactId);
            Assert.Equal(expectedResult, result);
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("MockId-1", "MockId-1-Contact", true)]
        [InlineData("MockId-2", "MockId-2-Contact", false)]
        public void AcceptContactRequest(string userId, string contactId, bool expectedResult)
        {
            var result = _contactViewModel.AcceptContactRequest(userId, contactId);
            Assert.Equal(expectedResult, result);
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("MockId-1", "MockId-1-Contact", true)]
        [InlineData("MockId-2", "MockId-2-Contact", false)]
        public void RemoveContact(string userId, string contactId, bool expectedResult)
        {
            var result = _contactViewModel.RemoveContact(userId, contactId);
            Assert.Equal(expectedResult, result);
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("MockId-1", "MockId-1-Contact", true)]
        [InlineData("MockId-2", "MockId-2-Contact", false)]
        public void BlockContact(string userId, string contactId, bool expectedResult)
        {
            var result = _contactViewModel.BlockContact(userId, contactId);
            Assert.Equal(expectedResult, result);
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData("MockId-1", "MockId-1-Contact", true)]
        [InlineData("MockId-2", "MockId-2-Contact", false)]
        public void IsContact(string userId, string contactId, bool expectedResult)
        {
            var result = _contactViewModel.IsContact(userId, contactId);
            Assert.Equal(expectedResult, result);
        }
    }
}
