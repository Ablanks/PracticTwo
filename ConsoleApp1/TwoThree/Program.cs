using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1-2
            /* 
            Workers worker = new Workers();
            worker.Name = "Андрей";
            worker.Surname = "Пронь";
            worker.Days = 40;
            worker.Rate = 300;
            Console.WriteLine(worker.GetSalary());
            */
            // 3
            Calculation calculate = new Calculation();
            calculate.SetCalculationLine("125");
            calculate.SetLastCalculationLine = "1";
            calculate.GetCalculationLine();
            Console.WriteLine(calculate.GetLastSymbole);
            calculate.DeleteLastSymbol();
            calculate.GetCalculationLine();
        }
    }
}