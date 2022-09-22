using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Helpers;
using WeatherApp.Interfaces;
using WeatherApp.Models;

namespace WeatherApp.Repos
{
    public class WeatherRepo : IWeatherRepo
    {
        public enum Unit
        {
            Standard,
            Metric,
            Imperial
        }
        string open_weather_api_key = Secrets.OpenWeatherApiKey;
        string open_weather_base_address = "api.openweathermap.org/data/2.5/weather";
        string protocol = "https://";

        public static string[] cities_names = { "Opatija", "London", "Novalja", "Pula", "Cres", "Istanbul", "Milan" };
        public static string[] countries = { "Croatia", "UK", "Croatia", "Croatia", "Croatia", "Turkey", "Italy" };

        public WeatherRepo()
        {

        }

        public async Task<OpenWeatherResponse> GetWeatherByLocation(string city, string country, Unit metric)
        {
            string parameters = $"?q={city},{country}&units={metric}&APPID={open_weather_api_key}";

            var client = new HttpClient();
            client.BaseAddress = new Uri($"{protocol}{open_weather_base_address}");
            var response = await client.GetAsync(parameters);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            string content = await response.Content.ReadAsStringAsync();
            OpenWeatherResponse openWeatherResponse = JsonSerializer.Deserialize<OpenWeatherResponse>(
                content, options);

            if (response.IsSuccessStatusCode) return openWeatherResponse;

            OpenWeatherResponse error_response = new OpenWeatherResponse
            {
                Weather = new OpenWeatherResponse.CurrentWeather[1]
            };
            error_response.Weather[0] = new OpenWeatherResponse.CurrentWeather
            {
                Description = response.ReasonPhrase
            };

            return error_response;
        }
    }
}
