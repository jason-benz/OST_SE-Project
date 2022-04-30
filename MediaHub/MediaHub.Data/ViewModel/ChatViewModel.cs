using MediaHub.Data.Model;
using MediaHub.Data.Persistency;

namespace MediaHub.Data.ViewModel;

public class ChatViewModel : IChatViewModel
{
    private readonly IChatDataManager _chatDataManager;
    private readonly IUserProfileDataManager _userProfileDataManager;
    private UserProfile _sender { get; set; }
    private UserProfile _receiver { get; set; }

    public ChatViewModel(IChatDataManager chatDataManager, IUserProfileDataManager userProfileDataManager)
    {
        _chatDataManager = chatDataManager;
        _userProfileDataManager = userProfileDataManager;
    }

    public void SetSenderById(string userId)
    {
        _sender = _userProfileDataManager.GetUserProfileById(userId);
    }

    public void SetReceiverById(string userId)
    {
        _receiver = _userProfileDataManager.GetUserProfileById(userId);
    }

    public List<UserProfile> GetAllContactUserProfiles()
    {
        // FIXME REMOVE THE SENDER FROM THIS LIST
        // var wrappedSender = new List<UserProfile> {sender};
        var tmp = _userProfileDataManager.GetAllUserProfiles();
        tmp.Remove(_sender);
        return tmp;
    }

    public List<Message> GetAllMessagesForActiveChat()
    {
        return _chatDataManager.GetMessagesBetweenTwoUsers(_receiver.UserId, _sender.UserId);
    }

    // TODO Add parameters
    public void InsertMessage(string content)
    {
        if (_sender != null && _receiver != null)
        {
            UserProfile senderProfile = _sender;
            UserProfile receiverProfile = _receiver;
            DateTime timeSent = DateTime.Now;
            Message m = new Message();
            m.Content = content;
            m.Sender = senderProfile;
            m.Receiver = receiverProfile;
            m.TimeSent = timeSent;
            _chatDataManager.InsertMessage(m);
        }
    }
}