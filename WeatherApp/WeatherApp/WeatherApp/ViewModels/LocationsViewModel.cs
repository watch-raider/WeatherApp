using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using WeatherApp.Models;

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

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public LocationsViewModel(List<City> cities)
        {
            Cities = new ObservableCollection<City>(cities);
            OnPropertyChanged(nameof(Cities));
        }
    }
}
