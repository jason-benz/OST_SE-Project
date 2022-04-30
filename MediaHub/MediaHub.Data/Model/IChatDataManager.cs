namespace MediaHub.Data.Model;

public interface IChatDataManager
{
    public List<Message> GetAllMessages();

    public void InsertMessage(Message message);
    // TODO

    public List<Message> GetMessagesBetweenTwoUsers(string userId1, string userId2);
}