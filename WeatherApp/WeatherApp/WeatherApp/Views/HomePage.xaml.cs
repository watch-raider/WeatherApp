﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            await Navigation.PushAsync(new LoadingPage());

            List<City> cities = new List<City>();

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                cities = await hvm.GetAllCities();
            }
            else
            {
                await DisplayAlert("Network Error", "Weather information may not be up to date", "OK");
            };

            await Task.Delay(100);
            await Navigation.PopAsync();

            await Task.Delay(100);
            await Navigation.PushAsync(new LocationsPage(cities));
        }
    }
}
