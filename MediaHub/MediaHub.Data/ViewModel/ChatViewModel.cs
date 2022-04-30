using MediaHub.Data.Model;
using MediaHub.Data.Persistency;

namespace MediaHub.Data.ViewModel;

public class ChatViewModel : IChatViewModel
{
    private readonly IChatDataManager _chatDataManager;
    private readonly IUserProfileDataManager _userProfileDataManager;
    private UserProfile sender { get; set; }
    private UserProfile receiver { get; set; }
    
    public ChatViewModel(IChatDataManager chatDataManager, IUserProfileDataManager userProfileDataManager)
    {
        _chatDataManager = chatDataManager;
        _userProfileDataManager = userProfileDataManager;

    }

    public void SetSenderById(string userId)
    {
        receiver = _userProfileDataManager.GetUserProfileById(userId);
    }
    
    public void SetReceiverById(string userId)
    {
        sender = _userProfileDataManager.GetUserProfileById(userId);
    }

    public List<UserProfile> GetAllContactUserProfiles()
    {
        // FIXME REMOVE THE SENDER FROM THIS LIST
       // var wrappedSender = new List<UserProfile> {sender};
        return _userProfileDataManager.GetAllUserProfiles();
    }

    public List<Message> GetAllMessagesForActiveChat()
    {
        return _chatDataManager.GetMessagesBetweenTwoUsers(receiver.UserId, sender.UserId);
    }
    
    // TODO Remove and replace with actual logic
    public List<Message> GetAllMessages()
    {
        // TODO this method must set the time received (= time received in frontend)
        return _chatDataManager.GetAllMessages();
    }
    
    // TODO Add parameters
    public void InsertMessage(string content)
    {
        UserProfile senderProfile = receiver;
        UserProfile receiverProfile = sender;
        DateTime timeSent = DateTime.Now;
        Message m = new Message();
        m.Content = content;
        m.Sender = senderProfile;
        m.Receiver = receiverProfile;
        m.TimeSent = timeSent;
        _chatDataManager.InsertMessage(m);
    }
}