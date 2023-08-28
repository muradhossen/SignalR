namespace SignalR.SignalRHub
{
    public interface INotificationHub
    { 
        Task OnConnectNotification(string eventName, string ConnectionId);
        Task ReciveNotification(string eventName, string message);
    }
}
