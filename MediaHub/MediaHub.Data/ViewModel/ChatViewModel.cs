using MediaHub.Data.Model;
using MediaHub.Data.Persistency;

namespace MediaHub.Data.ViewModel;

public class ChatViewModel : IChatViewModel
{
    private readonly IChatDataManager _chatDataManager;
    private readonly IUserProfileDataManager _userProfileDataManager;

    public ChatViewModel(IChatDataManager chatDataManager, IUserProfileDataManager userProfileDataManager)
    {
        _chatDataManager = chatDataManager;
        _userProfileDataManager = userProfileDataManager;

    }
    
    // TODO Remove and replace with actual logic
    public List<Message> GetAllMessages()
    {
        // TODO this method must set the time received (= time received in frontend)
        return _chatDataManager.GetAllMessages();
    }
    
    // TODO Add parameters
    public void InsertMessage(string senderProfileId, string receiverProfileId, string content)
    {
        UserProfile senderProfile = _userProfileDataManager.GetUserProfileById(senderProfileId);
        UserProfile receiverProfile = _userProfileDataManager.GetUserProfileById(receiverProfileId);
        DateTime timeSent = DateTime.Now;
        Message m = new Message();
        m.Content = content;
        m.Sender = senderProfile;
        m.Receiver = receiverProfile;
        m.TimeSent = timeSent;
        _chatDataManager.InsertMessage(m);
    }
}