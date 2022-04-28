using MediaHub.Data.Model;
using Microsoft.EntityFrameworkCore;

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
        context.Entry(m.Sender).State = EntityState.Unchanged;
        if (m.Receiver != null)
        {
            context.Entry(m.Receiver).State = EntityState.Unchanged;
        }
        
        context.Add(m); 
        context.SaveChanges();
    }
}