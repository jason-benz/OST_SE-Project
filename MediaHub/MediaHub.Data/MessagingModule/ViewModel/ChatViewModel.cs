using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.MessagingModule.Model;
using MediaHub.Data.ProfileModule.Model;
using Microsoft.AspNetCore.SignalR.Client;

namespace MediaHub.Data.MessagingModule.ViewModel;

public class ChatViewModel : IChatViewModel
{
    private IChatDataManager _chatDataManager;
    private IUserProfileDataManager _userProfileDataManager;
    private IContactDataManager _contactDataManager;

    public UserProfile? Sender { get; private set; }
    public UserProfile? Receiver { get; private set; }

    public ChatViewModel(IChatDataManager chatDataManager, IUserProfileDataManager userProfileDataManager, IContactDataManager contactDataManager)
    {
        _chatDataManager = chatDataManager;
        _userProfileDataManager = userProfileDataManager;
        _contactDataManager = contactDataManager;
    }

    public void SetSenderById(string userId)
    {
        Sender = _userProfileDataManager.GetUserProfileByIdLazyLoading(userId);
    }

    public void SetReceiverById(string userId)
    {
        Receiver = _userProfileDataManager.GetUserProfileByIdLazyLoading(userId);
    }

    public IEnumerable<UserProfile> GetAllContactUserProfiles(string userId)
    {
        var contactIds = _contactDataManager.GetContactIds(userId);
        contactIds.Remove(Sender.UserId);
        return _userProfileDataManager.GetUserProfilesById(contactIds);
    }

    public List<Message> GetAllMessagesForActiveChat()
    {
        return _chatDataManager.GetMessagesBetweenTwoUsers(Receiver.UserId, Sender.UserId);
    }

    public Message? InsertMessage(string content)
    {
        if (Sender == null || Receiver == null)
        {
            return null;
        }

        UserProfile senderProfile = Sender;
        UserProfile receiverProfile = Receiver;
        DateTime timeSent = DateTime.Now;
        Message m = new ();
        m.Content = content;
        m.Sender = senderProfile;
        m.Receiver = receiverProfile;
        m.TimeSent = timeSent;
        _chatDataManager.InsertMessage(m);
        return m;
    }
}