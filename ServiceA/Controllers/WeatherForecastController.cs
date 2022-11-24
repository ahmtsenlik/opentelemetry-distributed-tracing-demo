using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("TestRequest")]
        public async Task<IActionResult> Test()
        {
            string responseBody;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7105/");
                //HTTP GET
                var responseTask = await client.GetAsync("WeatherForecast");
                responseTask.EnsureSuccessStatusCode();
                responseBody = await responseTask.Content.ReadAsStringAsync();
            }
            return Ok();
        }
    }
}