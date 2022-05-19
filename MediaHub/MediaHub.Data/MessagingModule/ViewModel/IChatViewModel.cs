using MediaHub.Data.MessagingModule.Model;
using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Data.MessagingModule.ViewModel;

public interface IChatViewModel
{
    public UserProfile Receiver { get; }
    public UserProfile Sender { get; }
    public Message? InsertMessage(string content);
    public List<Message> GetAllMessagesForActiveChat();
    public void SetReceiverById(string userId);
    public void SetSenderById(string userId);
    public IEnumerable<UserProfile> GetAllContactUserProfiles(string userId);
}