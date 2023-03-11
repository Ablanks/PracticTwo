using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using Telegram.Bot.Types.Enums;

public class WeatherData
{
    public Coord coord { get; set; }
    public List<Weather> weather { get; set; }
    public string @base { get; set; }
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

public class FeatureProperties
{
    public double lon { get; set; }
    public double lat { get; set; }
}

public class Feature
{
    public FeatureProperties properties { get; set; }
}

public class FeatureCollection
{
    public List<Feature> features { get; set; }
}
namespace ConsoleApp2
{
    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("6017130190:AAHkzgOBB1iYZkMRQbs96pcMjLl4eBdlpkw");
        
        static string gd = "";
        static private string GetWindDirection(int degrees)
        {
            string[] directions = { "С", "ССВ", "СВ", "ВСВ", "В", "ВЮВ", "ЮВ", "ЮЮВ", "Ю", "ЮЮЗ", "ЮЗ", "ЗЮЗ", "З", "ЗСЗ", "СЗ", "ССЗ" };
            return directions[(int)Math.Round(((double)degrees % 360) / 22.5)];
        }

        static private string Forecast()
        {
            var (lon, lan) = Place();
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lon}&lon={lan}&units=metric&lang=ru&appid=cb1fe9bc41347598cd02c36d7d71b6ee";
            string json = new WebClient().DownloadString(url);
            WeatherData data = JsonConvert.DeserializeObject<WeatherData>(json);
            string weatherMain = data.weather[0].description;
            string windDirection = GetWindDirection(data.wind.deg);
            string s = "";
            s += $"{data.name}, температура {data.main.temp}, ощущается как {data.main.feels_like}," +
                              $" {weatherMain}, процент облачности воздуха {data.clouds.all} %, атмосферное давление {data.main.pressure} гПа," +
                              $" относительная влажность воздуха {data.main.humidity} % \n" + $"cкорость ветра {data.wind.speed}, направление {windDirection}";
            return s;
        }

        static private (double, double) Place()
        {
            string url = 
                $"https://api.geoapify.com/v1/geocode/search?text=новосибирск&apiKey=786944f1db4349f9858745d19e756b49";
            string json = new WebClient().DownloadString(url);
            FeatureCollection featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(json);

            double longitude = featureCollection.features[0].properties.lon;
            double latitude = featureCollection.features[0].properties.lat;
            return (latitude, longitude);
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text == "/start")
                {
                    SendInline(botClient: botClient, chatId: message.Chat.Id, cancellationToken: cancellationToken);
                    return;
                }
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                string codeOfButton = update.CallbackQuery.Data;
                if (codeOfButton == "weather")
                {
                    Console.WriteLine("Вызов погоды");
                    string telegramMessage = "Текущая погода в городе " + Forecast();
                    await botClient.SendTextMessageAsync(chatId: update.CallbackQuery.Message.Chat.Id, telegramMessage,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                }

                if (codeOfButton == "defaultCity")
                {
                    Console.WriteLine("Запрос на смену города");
                }
            }
        }



        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }
        public static async void SendInline(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(
                // keyboard
                new[]
                {
                    // first row
                  /*  new[]
                    {
                        // first button in row
                        InlineKeyboardButton.WithCallbackData(text: "Поменять текущий город", callbackData: "defaultCity"),
                        // second button in row
                        InlineKeyboardButton.WithCallbackData(text: "Поменять время отправления уведомлений", callbackData: "defaultNotification"),
                    },
                    */// second row
                    new[]
                    {
                        // first button in row
                        InlineKeyboardButton.WithCallbackData(text: "Текущая погода", callbackData: "weather")
                    },

                });

            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Привет",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }


        
        
        
    

        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}