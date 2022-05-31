using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.ChatModule.Model;
using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Data.ChatModule.ViewModel;

public class ChatViewModel : IChatViewModel
{
    private readonly IChatDataManager _chatDataManager;
    private readonly IUserProfileDataManager _userProfileDataManager;
    private readonly IContactDataManager _contactDataManager;

    public event Action? RefreshRequested;
    public UserProfile? User { get; private set; }
    public UserProfile? Contact { get; private set; }
    public string CurrentMessage { get; set; }
    public IEnumerable<UserProfile> ContactList { get; set; }
    public IEnumerable<Message> Messages { get; set; }
    

    public ChatViewModel(IChatDataManager chatDataManager, IUserProfileDataManager userProfileDataManager, IContactDataManager contactDataManager)
    {
        CurrentMessage = string.Empty;
        Messages = new List<Message>();
        ContactList = new List<UserProfile>();
        _chatDataManager = chatDataManager;
        _userProfileDataManager = userProfileDataManager;
        _contactDataManager = contactDataManager;
    }

    public void OpenChat(string contactUserId)
    {
        Contact = _userProfileDataManager.GetUserProfileByIdLazyLoading(contactUserId);
        LoadAllMessagesForActiveChat();
        RefreshRequested?.Invoke();
    }

    public void SetUserById(string userId)
    {
        User = _userProfileDataManager.GetUserProfileByIdLazyLoading(userId);
    }

    public void LoadAllContactUserProfiles(string userId)
    {
        var contactIds = _contactDataManager.GetContactIds(userId);
        contactIds.Remove(User!.UserId);
        ContactList = _userProfileDataManager.GetUserProfilesById(contactIds);
    }

    public void SendMessage()
    {
        if (User != null && Contact != null && CurrentMessage != string.Empty)
        {
            InsertMessage(CurrentMessage);
            CurrentMessage = string.Empty;
            LoadAllMessagesForActiveChat();
            RefreshRequested?.Invoke();
        }
    }

    public void LoadAllMessagesForActiveChat()
    {
        if (Contact != null && User != null)
        {
            var oldMessageCount = Messages.Count();
            Messages = _chatDataManager.GetMessagesBetweenTwoUsers(Contact.UserId, User.UserId);

            if (oldMessageCount != Messages.Count())
            {
                RefreshRequested?.Invoke();
            }
        }
    }

    private void InsertMessage(string content)
    {
        Message message = new()
        {
            Content = content,
            Sender = User!,
            Receiver = Contact!,
            TimeSent = DateTime.Now
        };

        _chatDataManager.InsertMessage(message);
    }
}