using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel;

public interface IChatViewModel
{
    public List<Message> GetAllMessages();
    public void InsertMessage(string senderProfileId, string receiverProfileId, string content);
}