using Backend.Constants;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs;

public class DataHub : Hub
{
    public async Task SendMessageAsync(string message, string fromUserId, string toUserId)
    {
        await Clients.User(fromUserId).SendAsync(ApplicationConstants.SignalR.ReceiveMessage, "test", message);
        await Clients.User(toUserId).SendAsync(ApplicationConstants.SignalR.ReceiveMessage, "test", message);
    }

    public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
    {
        await Clients.User(receiverUserId).SendAsync(ApplicationConstants.SignalR.ReceiveChatNotification, message, receiverUserId, senderUserId);
    }

}

