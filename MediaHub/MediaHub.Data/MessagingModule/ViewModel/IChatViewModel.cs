using MediaHub.Data.MessagingModule.Model;
using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Data.MessagingModule.ViewModel;

public interface IChatViewModel
{
    public IEnumerable<UserProfile> ContactList { get; set; }
    public UserProfile? Contact { get; }
    public void OpenChat(string contactUserId);
    public void SetUserById(string userId);
    public event Action RefreshRequested;
    public void LoadAllContactUserProfiles(string userId);
    public IEnumerable<Message> Messages { get; set; }
    public void SendMessage();
    public string CurrentMessage { get; set; }
    public void LoadAllMessagesForActiveChat();
}