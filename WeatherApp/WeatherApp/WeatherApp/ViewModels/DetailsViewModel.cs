using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        City city;
        public City City 
        { 
            get => city; 
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public DetailsViewModel(City city)
        {
            City = city;
        }
    }
}
