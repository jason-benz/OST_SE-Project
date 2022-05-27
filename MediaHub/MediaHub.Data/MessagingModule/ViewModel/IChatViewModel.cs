using MediaHub.Data.MessagingModule.Model;
using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Data.MessagingModule.ViewModel;

public interface IChatViewModel
{
    public event Action RefreshRequested;

    public UserProfile? Contact { get; }
    
    public string CurrentMessage { get; set; }
    
    public IEnumerable<UserProfile> ContactList { get; set; }
    
    public IEnumerable<Message> Messages { get; set; }
    
    public void OpenChat(string contactUserId);

    public void SetUserById(string userId);
    
    public void LoadAllContactUserProfiles(string userId);
    
    public void SendMessage();
    
    public void LoadAllMessagesForActiveChat();
}