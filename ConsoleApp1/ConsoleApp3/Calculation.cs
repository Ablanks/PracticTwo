using System;

namespace ConsoleApp2
{
    public class Calculation
    {
        private string calculationLine;

        public void SetCalculationLine(string line)
        {
            this.calculationLine = line;
        }

        public string SetLastCalculationLine
        {
            set
            {
                calculationLine = calculationLine.Insert(calculationLine.Length, value);
            }
        }

        public char GetLastSymbole
        {
            get
            {
                return calculationLine[calculationLine.Length - 1];
            }
        }

        public void GetCalculationLine()
        {
            Console.WriteLine(calculationLine);
        }

        public void DeleteLastSymbol()
        {
            this.calculationLine = calculationLine.Remove(calculationLine.Length - 1);
        }
    }
}