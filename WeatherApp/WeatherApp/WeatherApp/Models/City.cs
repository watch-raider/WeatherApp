using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace WeatherApp.Models
{
    public class City : INotifyPropertyChanged
    {
        [PrimaryKey]
        public int Id { get; set; }
        string name;
        public string Name 
        {
            get => name; 
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            } 
        }
        string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        string temperature;
        public string Temperature
        {
            get => temperature;
            set
            {
                temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }
        string feelsLike;
        public string FeelsLike
        {
            get => feelsLike;
            set
            {
                feelsLike = value;
                OnPropertyChanged(nameof(FeelsLike));
            }
        }
        string wind;
        public string Wind
        {
            get => wind;
            set
            {
                wind = value;
                OnPropertyChanged(nameof(Wind));
            }
        }
        string humidity;
        public string Humidity
        {
            get => humidity;
            set
            {
                humidity = value;
                OnPropertyChanged(nameof(Humidity));
            }
        }
        string time;
        public string Time
        {
            get => time;
            set
            {
                time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
        public string State { get; set; }
        public string Icon 
        {
            get
            {
                switch (this.State)
                {
                    case "Clouds":
                        return "\uf0c2";
                    case "Rain":
                        return "\uf740";
                    case "Drizzle":
                        return "\uf73d";
                    case "Thunderstorm":
                        return "\uf76c";
                    default:
                        return "\uf185";
                }
            }
        }
        public string IconColor 
        {
            get
            {
                switch (this.State)
                {
                    case "Clouds":
                        return "#808080";
                    case "Rain":
                        return "#000000";
                    case "Drizzle":
                        return "#808080";
                    case "Thunderstorm":
                        return "#000000";
                    default:
                        return "#ffbf00";
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
