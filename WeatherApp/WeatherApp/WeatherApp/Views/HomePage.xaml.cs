using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Assets;
using WeatherApp.Models;
using WeatherApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.Views
{
    public partial class HomePage : ContentPage
    {
        HomeViewModel hvm;

        public HomePage()
        {
            InitializeComponent();

            hvm = new HomeViewModel();

            WeatherBtn.Clicked += WeatherBtn_Clicked;
        }

        private async void WeatherBtn_Clicked(object sender, EventArgs e)
        {
            string open_weather_api_key;
            try
            {
                open_weather_api_key = await SecureStorage.GetAsync("key");
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
                open_weather_api_key = string.Empty;
            }

            if (string.IsNullOrEmpty(open_weather_api_key))
            {
                string key = await DisplayPromptAsync("OpenWeather", "Enter API key");
                try
                {
                    await SecureStorage.SetAsync("key", key);
                }
                catch (Exception ex)
                {
                    // Possible that device doesn't support secure storage on device.
                    await DisplayAlert("Error", ex.Message, "OK");
                    return;
                }
            }

            await Navigation.PushAsync(new LoadingPage());

            List<City> cities = new List<City>();

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    cities = await hvm.GetAllCities();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Please check API key is correct", "OK");
                    SecureStorage.RemoveAll();
                    await Navigation.PopAsync();
                    return;
                }
                await CityRepo.instance.DeleteAll();
                await CityRepo.instance.AddAll(cities);
            }
            else
            {
                IEnumerable<City> cities_db = await CityRepo.instance.GetAll();
                cities = cities_db.ToList();
                if (cities == null || cities.Count <= 0)
                {
                    await Task.Delay(100);
                    await Navigation.PopAsync();
                    await DisplayAlert("Network Error", "Internet connection required when connecting for the first time", "OK");
                    return;
                }
                await DisplayAlert("Network Error", "Weather information may not be up to date", "OK");
            };

            await Task.Delay(100);
            await Navigation.PopAsync();

            await Task.Delay(100);
            await Navigation.PushAsync(new LocationsPage(cities));
        }
    }
}
