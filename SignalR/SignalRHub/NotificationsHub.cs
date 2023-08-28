using Microsoft.AspNetCore.SignalR;

namespace SignalR.SignalRHub
{
    public class NotificationsHub : Hub<INotificationHub>
    {
        #region Without typesefty

        //public async Task CallFromClient(string content)
        //{ 

        //    await Clients.All.SendAsync("ReceiveNotification", "Server method called");
        //}

        //public override Task OnConnectedAsync()
        //{
        //    base.OnConnectedAsync();

        //    Clients.All.SendAsync("ConnectedNotification", Context.ConnectionId);

        //    return Task.CompletedTask;
        //}
        #endregion


        #region WithTypeSafty
        public async Task CallFromClient(string content)
        {

            await Clients.All.ReciveNotification("ReceiveNotification", "Server method called");
        }

        public override Task OnConnectedAsync()
        {
            base.OnConnectedAsync();

            Clients.All.OnConnectNotification("ConnectedNotification", Context.ConnectionId);

            return Task.CompletedTask;
        }
        #endregion



    }
}
