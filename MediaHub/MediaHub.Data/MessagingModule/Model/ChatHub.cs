using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MediaHub.Data.MessagingModule.Model;

//[Authorize]
public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessage(Message message)
    {
        await Clients.All.ReceiveMessage(message);
        // await Clients.User(message.Receiver.UserId).ReceiveMessage(message);
    }
}


