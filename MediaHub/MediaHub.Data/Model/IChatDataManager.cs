namespace MediaHub.Data.Model;

public interface IChatDataManager
{
    public List<Message> GetAllMessages();

    public void InsertMessage(Message message);
    // TODO
}