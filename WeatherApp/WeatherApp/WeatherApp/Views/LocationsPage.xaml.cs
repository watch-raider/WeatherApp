using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationsPage : ContentPage
    {
        LocationsViewModel lvm;

        public LocationsPage(List<City> cities)
        {
            InitializeComponent();

            lvm = new LocationsViewModel(cities);

            CityList.ItemSelected += CityList_ItemSelected;
            CityList.Refreshing += CityList_Refreshing;

            this.BindingContext = lvm;
        }

        private async void CityList_Refreshing(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await lvm.GetAllCities();
            }
            else
            {
                await DisplayAlert("Network Error", "Unable to refresh list due to no internet connection. Please make sure you are connected and try again", "OK");
            }

            CityList.IsRefreshing = false;
        }

        private async void CityList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //checks that there is a selected item
            if (e.SelectedItem == null)
                return;

            City city = e.SelectedItem as City;

            await Task.Delay(100);
            await Navigation.PushAsync(new DetailsPage(city));
            CityList.SelectedItem = null;
        }
    }
}