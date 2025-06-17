//Use the code in this class to prove that the SplitMix64 PRNG implemented is 
//indeed correct and intractaable as described in Task 2 of the Assignment Brief

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DSA___A2___Part_Soution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var prng = new SplitMix64();

            List<ulong> numbers = new List<ulong>();
            for (int i = 0; i < 100; i++)
                numbers.Add(prng.Next(1, 1000));

            bool allValid = numbers.All(n => n >= 1 && n <= 1000);
            bool notAscending = !IsSorted(numbers, true);
            bool notDescending = !IsSorted(numbers, false);

            Console.WriteLine($"All numbers in 1-1000: {allValid}");
            Console.WriteLine($"Not ascending: {notAscending}");
            Console.WriteLine($"Not descending: {notDescending}");

            int[] sizes = { 1000, 10000, 100000, 1000000 };
            Dictionary<int, double> timings = new Dictionary<int, double>();

            foreach (int size in sizes)
            {
                prng.Next(1, 1000);

                int trials = 10;
                long totalTicks = 0;

                for (int t = 0; t < trials; t++)
                {
                    var sw = Stopwatch.StartNew();
                    for (int i = 0; i < size; i++)
                        prng.Next(1, 1000);
                    sw.Stop();
                    totalTicks += sw.ElapsedTicks;
                }

                double avgMs = (totalTicks * 1000.0) / (Stopwatch.Frequency * trials);
                timings.Add(size, avgMs);
                Console.WriteLine($"Size: {size,7} | Time: {avgMs:F4} ms");
            }

            Console.WriteLine("\nCSV Data:");
            Console.WriteLine("Size,Time(ms)");
            foreach (var kv in timings)
                Console.WriteLine($"{kv.Key},{kv.Value}");
        }

        private static bool IsSorted(List<ulong> list, bool ascending)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (ascending && list[i] > list[i + 1])
                    return false;
                if (!ascending && list[i] < list[i + 1])
                    return false;
            }
            return true;
        }
    }
}