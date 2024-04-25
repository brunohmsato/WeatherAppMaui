using MauiApp1.Models;
using Newtonsoft.Json;

namespace MauiApp1.Services
{
    public static class ApiService
    {
        private static readonly string Secret = "f059533fdfc77e9ea080f86d4cd5f3cf";

        public static async Task<Root?> GetWeatherByCoordAsync(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&units=metric&appid={Secret}");

            return JsonConvert.DeserializeObject<Root>(response);
        }

        public static async Task<Root?> GetWeatherByCityNameAsync(string cidade)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?q={cidade}&units=metric&appid={Secret}");

            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
