using Microsoft.AspNetCore.SignalR;

namespace MediaHub.Data.MessagingModule.Model;

public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessage(Message message)
    {
        await Clients.All.ReceiveMessage(message);
        //await Clients.User(message.Receiver.UserId).ReceiveMessage(message);
        //await Clients.User(message.Receiver.UserId).SendAsync("ReceiveMessage", message);
    }
}


