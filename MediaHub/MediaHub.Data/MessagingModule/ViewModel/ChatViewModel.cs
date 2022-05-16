using MediaHub.Data.MessagingModule.Model;
using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Data.MessagingModule.ViewModel;

public class ChatViewModel : IChatViewModel
{
    private IChatDataManager _chatDataManager { get; set; }
    private IUserProfileDataManager _userProfileDataManager { get; set; }
    private UserProfile? _sender { get; set; }
    public UserProfile? Receiver { get; private set; }

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
        Receiver = _userProfileDataManager.GetUserProfileById(userId);
    }

    public List<UserProfile> GetAllContactUserProfiles()
    {
        var tmp = _userProfileDataManager.GetAllUserProfiles();
        tmp.Remove(_sender);
        return tmp;
    }

    public List<Message> GetAllMessagesForActiveChat()
    {
        return _chatDataManager.GetMessagesBetweenTwoUsers(Receiver.UserId, _sender.UserId);
    }

    public void InsertMessage(string content)
    {
        if (_sender != null && Receiver != null)
        {
            UserProfile senderProfile = _sender;
            UserProfile receiverProfile = Receiver;
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