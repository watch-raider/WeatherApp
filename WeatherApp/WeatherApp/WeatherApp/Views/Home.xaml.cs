using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp.Views
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();

            WeatherBtn.Clicked += WeatherBtn_Clicked;
        }

        private async void WeatherBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Locations());
        }
    }
}
