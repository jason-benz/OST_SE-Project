using MediaHub.Data.MessagingModule.Model;
using MediaHub.Data.PersistencyLayer;
using Microsoft.EntityFrameworkCore;

namespace MediaHub.Data.MessagingModule.Persistency;

public class ChatDataManager : IChatDataManager
{
    public List<Message> GetMessagesBetweenTwoUsers(string userId1, string userId2)
    {
        using MediaHubDBContext context = new();
        return context.Messages.Include(m => m.Sender).Include(m => m.Receiver).Where(m =>
            m.Receiver.UserId.Equals(userId1) && m.Sender.UserId.Equals(userId2) ||
            m.Receiver.UserId.Equals(userId2) && m.Sender.UserId.Equals(userId1)).OrderBy(m => m.TimeSent).ToList();
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