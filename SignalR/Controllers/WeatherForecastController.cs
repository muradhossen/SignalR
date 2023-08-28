using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.SignalRHub;

namespace SignalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHubContext<NotificationsHub> _hubContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , IHubContext<NotificationsHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            //await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Hello from API");
           var hub = new HubEvent();
            await hub.SendNotification(_hubContext,"Hello from API");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}