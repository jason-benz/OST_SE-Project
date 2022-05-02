using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel;

public interface IChatViewModel
{ 
    public void InsertMessage(string content);
    public List<Message> GetAllMessagesForActiveChat();
    public void SetReceiverById(string userId);
    public void SetSenderById(string userId);
    public List<UserProfile> GetAllContactUserProfiles();
}