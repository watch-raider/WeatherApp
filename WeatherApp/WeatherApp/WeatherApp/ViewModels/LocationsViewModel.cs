using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using WeatherApp.Repos;

namespace WeatherApp.ViewModels
{
    public class LocationsViewModel : INotifyPropertyChanged
    {
        ObservableCollection<City> cities;
        public ObservableCollection<City> Cities 
        { 
            get => cities; 
            set
            {
                cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }

        readonly IWeatherRepo weatherRepo;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public LocationsViewModel(List<City> cities)
        {
            weatherRepo = new WeatherRepo();

            Cities = new ObservableCollection<City>(cities);
            OnPropertyChanged(nameof(Cities));
        }

        public async Task GetAllCities()
        {
            Cities.Clear();

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

                Cities.Add(city);
            };
        }
    }
}
