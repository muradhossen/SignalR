using Microsoft.AspNetCore.SignalR;

namespace SignalR.SignalRHub
{
    public class NotificationsHub : Hub
    {
        public async Task SendNotification(string content)
        {
            await Clients.All.SendAsync("ReceiveNotification", content);
        }

    }
}
