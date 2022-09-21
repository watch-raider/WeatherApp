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
    public partial class DetailsPage : ContentPage
    {
        DetailsViewModel dvm;
        public DetailsPage(City city)
        {
            InitializeComponent();

            dvm = new DetailsViewModel(city);

            this.BindingContext = dvm;
        }
    }
}