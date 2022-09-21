using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using WeatherApp.Repos;

namespace WeatherApp.ViewModels
{
    public class HomeViewModel
    {
        readonly IWeatherRepo weatherRepo;

        public HomeViewModel()
        {
            weatherRepo = new WeatherRepo();
        }

        public async Task<List<City>> GetAllCities()
        {
            List<City> cities = new List<City>();

            for (int i = 0; i < WeatherRepo.cities_names.Length; i++)
            {
                OpenWeatherResponse response = await weatherRepo.GetWeatherByLocation(WeatherRepo.cities_names[i], WeatherRepo.countries[i], WeatherRepo.Unit.Metric);

                City city = new City
                {
                    Id = i,
                    Name = WeatherRepo.cities_names[i],
                    Description = response.Weather[0].Description,
                    Temperature = $"{response.Main.Temp}°",
                    FeelsLike = $"Feels like {response.Main.Feels_like}°",
                    Wind = $"Wind EN {response.Wind.Speed} m/s",
                    Humidity = $"Humidity {response.Main.Humidity}%",
                    Time = DateTime.Now.ToString("HH:mm")
                };

                cities.Add(city);
            };

            return cities;
        }
    }
}
