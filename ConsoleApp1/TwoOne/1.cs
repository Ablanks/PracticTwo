using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 1
            StringBuilder a = new StringBuilder("ab");;
            StringBuilder s = new StringBuilder("aabbccd");
            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    if (a[i] == s[j])
                    {
                        count++;
                    }
                }
            }
            Console.Write(count);
            */
            
            int[] nums = { 10, 1, 39, 7, 6, 1, 5 };
            int target = 50;

            List<List<int>> result = CombinationSum(nums, target);

            Console.WriteLine("Уникальные комбинации чисел, сумма которых равна цели:");
            foreach (List<int> combination in result)
            {
                Console.WriteLine(string.Join(", ", combination));
            }
        
            /* 3
            bool FindDuplicates(int[] nums)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    int count = 0;
                    for (int j = 0; j < nums.Length; j++)
                    {
                        if (nums[i] == nums[j])
                        {
                            count++;
                        }
                    }

                    if (count == 2)
                    {
                        return true;
                    }
                }
                return false;
            }
            int[] nums = new int[] { 1, 1, 2, 3, 4};
            Console.Write(FindDuplicates(nums));
            */
        }

        static List<List<int>> CombinationSum(int[] candidates, int target)
        {
            List<List<int>> result = new List<List<int>>();
            Array.Sort(candidates);
            FindCombinations(candidates, target, new List<int>(), result, 0);

            return result;
        }

        static void FindCombinations(int[] candidates, int target, List<int> current, List<List<int>> result, int start)
        {
            if (target == 0)
            {
                result.Add(new List<int>(current)); 
                
                return;
                
            }

            for (int i = start; i < candidates.Length && candidates[i] <= target; i++)
            {
                if (i > start && candidates[i] == candidates[i - 1])
                {
                    continue; 
                }

                current.Add(candidates[i]);
                FindCombinations(candidates, target - candidates[i], current, result, i + 1);
                current.RemoveAt(current.Count - 1);

            }
        }
    }
}