using System;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Вызовем метод для получения данных о водителях
            DatabaseRequests.GetTypeCarQuery();
            Console.WriteLine();
            // Добавим нового водителя в БД
            DatabaseRequests.AddDriverQuery("Денис", "Иванов", DateTime.Parse("1997.01.12"));
            // Вызовем метод для получения данных о водителях
            DatabaseRequests.GetDriverQuery();
        
            // Вызовем метод для получения данных о типах автомобилей
            DatabaseRequests.GetTypeCarQuery();
            Console.WriteLine();
            // Добавим новый тип автомобиля в БД
            DatabaseRequests.AddTypeCarQuery("Воздушный");
            // Вызовем метод для получения данных о типах автомобилей
            DatabaseRequests.GetTypeCarQuery();
            //
        }
    }
}