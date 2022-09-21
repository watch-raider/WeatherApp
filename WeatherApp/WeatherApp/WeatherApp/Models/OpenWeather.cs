using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class OpenWeather
    {

    }

    public class OpenWeatherResponse
    {
        public Coordinates Coord { get; set; }
        public class Coordinates
        {
            public long Longitude { get; set; }
            public long Latitude { get; set; }
        }
        public CurrentWeather[] Weather { get; set; }
        public class CurrentWeather
        {
            public long Id { get; set; }
            public string Main { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }
        }
        public string Base { get; set; }
        public MainWeather Main { get; set; }
        public class MainWeather
        {
            public double Temp { get; set; }
            public double Feels_like { get; set; }
            public double Temp_min { get; set; }
            public double Temp_max { get; set; }
            public double Pressure { get; set; }
            public double Humidity { get; set; }
            public double Sea_level { get; set; }
            public double Grnd_level { get; set; }
        }
        public long Visibility { get; set; }
        public WindDetails Wind { get; set; }
        public class WindDetails
        {
            public double Speed { get; set; }
            public double Deg { get; set; }
            public double Gust { get; set; }
        }
        public int Timezone { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }
}
