using System;
using WeatherApp.Assets;
using WeatherApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App(string filename)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());

            // sets up the SQLite database and different tables on the phone for storage
            CityRepo.Initialise(filename);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
