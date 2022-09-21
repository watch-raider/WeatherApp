using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Repos;

namespace WeatherApp.Interfaces
{
    public interface IWeatherRepo
    {
        Task<OpenWeatherResponse> GetWeatherByLocation(string city, string country, WeatherRepo.Unit metric);
    }
}
