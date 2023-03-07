using System;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Типы автомобилей");
            Console.WriteLine();
            DatabaseRequests.GetTypeCarQuery();
            Console.WriteLine();
            Console.WriteLine("Хотите добавить новый тип автомобиля?");
            string b = Console.ReadLine();
            if (b == "Yes" || b == "Да")
            {
                Console.WriteLine("Введите тип автомобиля");
                DatabaseRequests.AddTypeCarQuery(Console.ReadLine());
                DatabaseRequests.GetTypeCarQuery();
            }
            Console.WriteLine("Автомобили");
            DatabaseRequests.GetCarQuery();
            Console.WriteLine();
            
            
            Console.WriteLine("Хотите добавить новый автомобиль?");
            string v = Console.ReadLine();
            if (v == "Yes" || v == "Да")
            {
                Console.WriteLine("Введите id типа автомобиля, название автомобиля, штатное название, количество мест");
                DatabaseRequests.AddCarQuery(Convert.ToInt32(Console.ReadLine()), Console.ReadLine(), Console.ReadLine(), Convert.ToInt32(Console.ReadLine()));
                DatabaseRequests.GetCarQuery();
            }
            
            Console.WriteLine("Водители");
            DatabaseRequests.GetDriverQuery();
            Console.WriteLine();
            
            Console.WriteLine("Хотите добавить нового водителя?");
            string a = Console.ReadLine();
            if (a == "Yes" || a == "Да")
            {
                Console.WriteLine("Введите имя, фамилию и дату рождения водителя");
                DatabaseRequests.AddDriverQuery(Console.ReadLine(), Console.ReadLine(), DateTime.Parse(Console.ReadLine()));
            }
            
            Console.WriteLine("Права");
            DatabaseRequests.GetRightsCategoryQuery();
            
            Console.WriteLine();
            Console.WriteLine("Хотите добавить новую категорию прав?");
            string s = Console.ReadLine();
            if (s == "Yes" || s == "Да")
            {
                Console.WriteLine("Введите название категории прав, что бы добавить ее");
                DatabaseRequests.AddRightsCategoryQuery(Console.ReadLine());
                Console.WriteLine();
                DatabaseRequests.GetRightsCategoryQuery();
            }
            
           
            
            Console.WriteLine("Водители и их категории прав");
            DatabaseRequests.GetDriverRightsCategoryQuery();
            
            Console.WriteLine();
            Console.WriteLine("Хотите добавить новую категорию прав водителю?");
            string c = Console.ReadLine();
            if (c == "Yes" || c == "Да")
            {
                Console.WriteLine(
                    "Введите id водителя, затем введите id категории прав, чтобы выдать ему эту категорию прав ");
                DatabaseRequests.AddDriverRightsCategoryQuery(Convert.ToInt32(Console.ReadLine()),
                    Convert.ToInt32(Console.ReadLine()));
            }
            
            Console.WriteLine("Маршруты");
            DatabaseRequests.GetItineraryQuery();
            Console.WriteLine();
            Console.WriteLine("Хотите добавить новый маршрут?");
            string d = Console.ReadLine();
            if (d == "Yes" || d == "Да")
            {
                Console.WriteLine("Введите название маршрута");
                DatabaseRequests.AddItineraryQuery(Console.ReadLine());
                Console.WriteLine();
                DatabaseRequests.GetItineraryQuery();
            }
            
            Console.WriteLine("Рейсы");
            DatabaseRequests.GetRouteQuery();
            Console.WriteLine();
            Console.WriteLine("Хотите добавить новый рейс?");
            string x = Console.ReadLine();
            if (x == "Yes" || x == "Да")
            {
                Console.WriteLine("Введите id водителя, id машины, id маршрута и число пассажиров");
                DatabaseRequests.AddRouteQuery(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()), 
                    Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
                Console.WriteLine();
                DatabaseRequests.GetRouteQuery();
            }
        }
    }
}