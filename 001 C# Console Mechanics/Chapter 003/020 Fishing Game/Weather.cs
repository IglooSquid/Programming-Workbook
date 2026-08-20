using FishingGame;
using System;
using System.Collections.Generic;

namespace FishingGame
{
    public class Weather
    {
        public enum WeatherState
        {
            Sunny,
            Overcast,
            Rainy,
            Windy,
            Foggy
        }

        public static WeatherState GenerateWeather()
        {
            int weatherCount = Enum.GetNames(typeof(WeatherState)).Length;
            var random = new Random();
            int index = random.Next(weatherCount);
            WeatherState result = (WeatherState)index;
            return result;
        }
    }
}