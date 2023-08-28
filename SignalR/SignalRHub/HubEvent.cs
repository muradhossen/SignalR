using Microsoft.AspNetCore.SignalR;

namespace SignalR.SignalRHub
{
    public class HubEvent
    {
        public async Task SendNotification(IHubContext<NotificationsHub> hubContext, string content)
        {
            await hubContext.Clients.All.SendAsync("ReceiveNotification", content);
        }
    }
}
