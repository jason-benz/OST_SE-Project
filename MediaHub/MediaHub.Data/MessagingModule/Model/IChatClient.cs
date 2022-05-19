namespace MediaHub.Data.MessagingModule.Model;

public interface IChatClient
{
    Task ReceiveMessage(Message message);
}