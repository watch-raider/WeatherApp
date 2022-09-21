using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.ViewModels;
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

            this.BindingContext = lvm;
        }

        private async void CityList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //checks that there is a selected item
            if (e.SelectedItem == null)
                return;

            City city = e.SelectedItem as City;

            await Task.Delay(100);
            await Navigation.PushAsync(new DetailsPage(city));
        }
    }
}