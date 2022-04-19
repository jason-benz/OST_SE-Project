using MediaHub.Data.Model;

namespace MediaHub.Data.Persistency;

public class ChatDataManager : IChatDataManager
{
    // TODO Add interface in model 
    public List<Message> GetAllMessages() // TODO Remove this method
    {
        using MediaHubDBContext context = new();
        return context.Messages.ToList();
    }

    public void InsertMessage(Message m)
    {
        using MediaHubDBContext context = new();
        context.Add(m);
    }
}