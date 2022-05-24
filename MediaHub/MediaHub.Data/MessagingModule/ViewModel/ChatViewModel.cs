using MediaHub.Data.ContactsModule.Model;
using MediaHub.Data.MessagingModule.Model;
using MediaHub.Data.ProfileModule.Model;
using Microsoft.AspNetCore.SignalR.Client;

namespace MediaHub.Data.MessagingModule.ViewModel;

public class ChatViewModel : IChatViewModel
{
    private readonly IChatDataManager _chatDataManager;
    private readonly IUserProfileDataManager _userProfileDataManager;
    private readonly IContactDataManager _contactDataManager;

    public event Action? RefreshRequested;
    public UserProfile? User { get; private set; }
    public UserProfile? Contact { get; private set; }
    public string? CurrentMessage { get; set; }
    public IEnumerable<UserProfile> ContactList { get; set; }
    public IEnumerable<Message> Messages { get; set; }
    

    public ChatViewModel(IChatDataManager chatDataManager, IUserProfileDataManager userProfileDataManager, IContactDataManager contactDataManager)
    {
        CurrentMessage = "";
        Messages = new List<Message>();
        ContactList = new List<UserProfile>();
        _chatDataManager = chatDataManager;
        _userProfileDataManager = userProfileDataManager;
        _contactDataManager = contactDataManager;
    }

    public void OpenChat(string contactUserId)
    {
        SetContactById(contactUserId);
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
        contactIds.Remove(User.UserId);
        ContactList = _userProfileDataManager.GetUserProfilesById(contactIds);
    }

    public void SendMessage()
    {
        if (User != null && Contact != null && CurrentMessage != null)
        {
            InsertMessage(CurrentMessage);
            ResetCurrentMessage();
            LoadAllMessagesForActiveChat();
            RefreshRequested?.Invoke();
        }
    }

    public void LoadAllMessagesForActiveChat()
    {
        Messages = _chatDataManager.GetMessagesBetweenTwoUsers(Contact.UserId, User.UserId);
    }


    private void SetContactById(string userId)
    {
        Contact = _userProfileDataManager.GetUserProfileByIdLazyLoading(userId);
    }

    private void InsertMessage(string content)
    {
        Message message = new()
        {
            Content = content,
            Sender = User,
            Receiver = Contact,
            TimeSent = DateTime.Now
        };

        _chatDataManager.InsertMessage(message);
    }

    private void ResetCurrentMessage()
    {
        CurrentMessage = string.Empty;
    }
}