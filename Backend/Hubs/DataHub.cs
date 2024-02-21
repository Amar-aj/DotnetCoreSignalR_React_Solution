using Backend.Constants;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs;

public class DataHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    public async Task SendMessageAsync(string message, string fromUserId, string toUserId)
    {
        await Clients.User(fromUserId).SendAsync(ApplicationConstants.SignalR.ReceiveMessage, "test", message);
        await Clients.User(toUserId).SendAsync(ApplicationConstants.SignalR.ReceiveMessage, "test", message);
    }

    public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
    {
        await Clients.User(receiverUserId).SendAsync(ApplicationConstants.SignalR.ReceiveChatNotification, message, receiverUserId, senderUserId);
    }



    private static List<string> connectedClients = new List<string>();


    private static string myConnectionId = "";

    public override async Task OnConnectedAsync()
    {
        myConnectionId = Context.ConnectionId;
        connectedClients.Add(myConnectionId);
        await base.OnConnectedAsync();
        await Clients.All.SendAsync("ClientConnected", Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        connectedClients.Remove(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
        await Clients.All.SendAsync("ClientDisconnected", Context.ConnectionId);
    }

    public List<string> GetConnectedClients()
    {
        return connectedClients;
    }
    public string GetOwnConnectionId()
    {
        return myConnectionId;
    }

    public async Task SendPrivateMessage(string message, string fromUserId, string toUserId)
    {
        await Clients.Client(fromUserId).SendAsync("ReceivePrivateMessage", "From", message);
        await Clients.Client(toUserId).SendAsync("ReceivePrivateMessage", "To", message);
    }
}

