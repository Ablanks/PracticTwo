using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            //DatabaseRequests.AddTypeCarQuery("Космический");
            Console.WriteLine(DatabaseRequests.GetTypeCarQuery());
            //DatabaseRequests.AddDriverQuery("Семён", "Неяскин", DateTime.Parse("2000.05.21"));
            Console.WriteLine();
            Console.WriteLine(DatabaseRequests.GetDriverQuery());
            Console.WriteLine();
            //DatabaseRequests.AddRightsCategoryQuery("М");
            DatabaseRequests.GetRightsCategoryQuery();
            Console.WriteLine();
           // DatabaseRequests.AddDriverRightsCategoryQuery(6, 1);
            DatabaseRequests.GetDriverRightsCategoryQuery();
            Console.WriteLine();
            
        }
    }
}