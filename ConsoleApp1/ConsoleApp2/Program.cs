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
using Quartz;
using Quartz.Impl;
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

namespace ConsoleApp2
{
    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("6017130190:AAHkzgOBB1iYZkMRQbs96pcMjLl4eBdlpkw");
        
        static private string GetWindDirection(int degrees)
        {
            string[] directions = { "С", "ССВ", "СВ", "ВСВ", "В", "ВЮВ", "ЮВ", "ЮЮВ", "Ю", "ЮЮЗ", "ЮЗ", "ЗЮЗ", "З", "ЗСЗ", "СЗ", "ССЗ" };
            return directions[(int)Math.Round((double)degrees % 360 / 22.5)];
        }

        static private string Forecast(string b)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={b}" +
                         $"&units=metric&lang=ru&appid=cb1fe9bc41347598cd02c36d7d71b6ee";
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
        
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                DatabaseRequests.AddUser(message.Chat.Id);
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
                    string telegramMessage = "Текущая погода в городе " + Forecast("Tomsk");
                    await botClient.SendTextMessageAsync(chatId: update.CallbackQuery.Message.Chat.Id, telegramMessage,
                        parseMode: ParseMode.Html);
                }

                if (codeOfButton == "defaultCity")
                {
                    Console.WriteLine("Запрос на смену города");
                    string telegramMessage = "Напишите город по умолчанию";
                    await botClient.SendTextMessageAsync(chatId: update.CallbackQuery.Message.Chat.Id, telegramMessage,
                        parseMode: ParseMode.Html);
                    if (update.Type == UpdateType.Message)
                    {
                        DatabaseRequests.UpdUser("Москва", update.CallbackQuery.Message.Chat.Id);
                        Console.WriteLine("Москва");
                        return;
                    }
                }

                if (codeOfButton == "defaultNotification")
                {
                    
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
                    new[]
                    {
                        // first button in row
                        InlineKeyboardButton.WithCallbackData(text: "Поменять текущий город", callbackData: "defaultCity"),
                        // second button in row
                       // InlineKeyboardButton.WithCallbackData(text: "Поменять время отправления уведомлений", callbackData: "defaultNotification"),
                    },
                    // second row
                    new[]
                    {
                        // first button in row
                        InlineKeyboardButton.WithCallbackData(text: "Текущая погода", callbackData: "weather")
                    },

                });

            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Привет, чем могу помочь?",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }
        
        public class SendMessageJob : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {

                var mess = "Привет, это сообщение отправлено по расписанию! \n" +
                           "Погода в городе ";
                    await bot.SendTextMessageAsync(5345, mess);
            }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            var factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();

            await scheduler.Start();

            var job = JobBuilder.Create<SendMessageJob>()
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithDailyTimeIntervalSchedule
                (s =>
                    s.WithIntervalInHours(24)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(9, 20))
                )
                .Build();
            

            await scheduler.ScheduleJob(job, trigger);
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
