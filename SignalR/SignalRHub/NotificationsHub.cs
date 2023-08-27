using Microsoft.AspNetCore.SignalR;

namespace SignalR.SignalRHub
{
    public class NotificationsHub : Hub<INotificationHub>
    {
        #region Without typesefty
        //public async Task SendNotification(string content)
        //{
        //    await Clients.All.SendAsync("ReceiveNotification", content);
        //}

        //public async Task CallFromClient(string content)
        //{
        //    var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55)                 
        //    })
        //  .ToArray();

        //    await Clients.All.SendAsync("WeatherNotification", weather);
        //}

        //public override Task OnConnectedAsync()
        //{
        //    base.OnConnectedAsync();

        //    Clients.All.SendAsync("ReceiveNotification", $"Connection --> {Context.ConnectionId}");

        //    return Task.CompletedTask;
        //}
        #endregion


        public async Task SendNotification(string content)
        {
            await Clients.All.ReciveNotification("ReceiveNotification", content);
        }

        public async Task CallFromClient(string content)
        {
            var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55)
            })
          .ToArray();

            await Clients.All.SendWeatherNotification("WeatherNotification", weather);
        }

        public override Task OnConnectedAsync()
        {
            base.OnConnectedAsync();

            Clients.All.OnConnectNotification("ConnectedNotification", Context.ConnectionId);

            return Task.CompletedTask;
        }

    }
}
