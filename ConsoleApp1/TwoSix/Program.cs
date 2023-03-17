using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;

public class WeatherData
{
    public Coord coord { get; set; }
    public List<Weather> weather { get; set; }
    public Main main { get; set; }
    public int visibility { get; set; }
    public Wind wind { get; set; }
    public Rain rain { get; set; }
    public Clouds clouds { get; set; }
    public int dt { get; set; }
    public Sys sys { get; set; }
    public int timezone { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public int cod { get; set; }
}

public class Coord
{
    public double lon { get; set; }
    public double lat { get; set; }
}

public class Weather
{
    public int id { get; set; }
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }
}

public class Main
{
    public double temp { get; set; }
    public double feels_like { get; set; }
    public double temp_min { get; set; }
    public double temp_max { get; set; }
    public int pressure { get; set; }
    public int humidity { get; set; }
    public int sealevel { get; set; }
    public int grndlevel { get; set; }
}

public class Wind
{
    public double speed { get; set; }
    public int deg { get; set; }
    public double gust { get; set; }
}

public class Rain
{
    public double h { get; set; }
}

public class Clouds
{
    public int all { get; set; }
}

public class Sys
{
    public int type { get; set; }
    public int id { get; set; }
    public string country { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
}


namespace ConsoleApp2
{
    class Program
    {
        static private string GetWindDirection(int degrees)
        {
            string[] directions = { "С", "ССВ", "СВ", "ВСВ", "В", "ВЮВ", "ЮВ", "ЮЮВ", "Ю", "ЮЮЗ", "ЮЗ", "ЗЮЗ", "З", "ЗСЗ", "СЗ", "ССЗ" };
            return directions[(int)Math.Round(((double)degrees % 360) / 22.5)];
        }
        
        static void Main(string[] args)
        {
            string url = "https://api.openweathermap.org/data/2.5/weather?lat=56.5&lon=84.97&units=metric&lang=ru&appid=cb1fe9bc41347598cd02c36d7d71b6ee";
            string json = new WebClient().DownloadString(url);
            WeatherData data = JsonConvert.DeserializeObject<WeatherData>(json);
            string weatherMain = data.weather[0].description;
            string windDirection = GetWindDirection(data.wind.deg);
            Console.WriteLine($"{data.name}, температура {data.main.temp}, ощущается как {data.main.feels_like}," +
                              $" {weatherMain}, процент облачности воздуха {data.clouds.all} %, атмосферное давление {data.main.pressure} гПа," +
                              $" относительная влажность воздуха {data.main.humidity} %");
            Console.WriteLine( $"cкорость ветра {data.wind.speed}, направление {windDirection}");
        }
    }
}