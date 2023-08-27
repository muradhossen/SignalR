namespace SignalR.SignalRHub
{
    public interface INotificationHub
    {
        Task SendWeatherNotification(string eventName, WeatherForecast[] weathers);
        Task OnConnectNotification(string eventName, string ConnectionId);
        Task ReciveNotification(string eventName, string message);
    }
}
