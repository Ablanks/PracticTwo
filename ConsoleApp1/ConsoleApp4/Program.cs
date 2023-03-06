using System;
using System.Collections.Generic;


namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            string romanNumeral = "IV"; 
            int result = RomanToInt(romanNumeral);

            Console.WriteLine("{0} = {1}", romanNumeral, result);
        }

        static int RomanToInt(string s)
        {
            Dictionary<char, int> romanValues = new Dictionary<char, int>()
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 }
            };

            int result = 0;
            int prevValue = 0;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                int currentValue = romanValues[s[i]];

                if (currentValue < prevValue)
                {
                    result -= currentValue;
                }
                else
                {
                    result += currentValue;
                }

                prevValue = currentValue;
            }

            return result;
        }
    }
}