using MediaHub.Data.MessagingModule.Model;
using MediaHub.Data.ProfileModule.Model;
using Microsoft.AspNetCore.SignalR.Client;

namespace MediaHub.Data.MessagingModule.ViewModel;

public class ChatViewModel : IChatViewModel
{
    public UserProfile? Sender { get; private set; }
    public UserProfile? Receiver { get; private set; }
    private IChatDataManager _chatDataManager { get; set; }
    private IUserProfileDataManager _userProfileDataManager { get; set; }


    public ChatViewModel(IChatDataManager chatDataManager, IUserProfileDataManager userProfileDataManager)
    {
        _chatDataManager = chatDataManager;
        _userProfileDataManager = userProfileDataManager;
    }

    public void SetSenderById(string userId)
    {
        Sender = _userProfileDataManager.GetUserProfileById(userId);
    }

    public void SetReceiverById(string userId)
    {
        Receiver = _userProfileDataManager.GetUserProfileById(userId);
    }

    public List<UserProfile> GetAllContactUserProfiles()
    {
        var tmp = _userProfileDataManager.GetAllUserProfiles();
        tmp.Remove(Sender);
        return tmp;
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