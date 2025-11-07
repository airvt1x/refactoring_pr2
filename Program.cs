using System;
using System.Diagnostics;

class FinalComparison
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Сравнение производительности:\n");
        Console.WriteLine("Размер\tИсходный(мс)\tУлучшенный(мс)\tУскорение");
        
        int[] sizes = { 100, 200, 300, 400, 500, 1000 };
        
        foreach (int size in sizes)
        {
            int[] arr = GenerateArray(size);
            
            long totalTime1 = 0;
            int runs1 = 20;
            int result1 = 0;
            
            for (int i = 0; i < runs1; i++)
            {
                Stopwatch sw1 = Stopwatch.StartNew();
                result1 = FindMaxSubarray(arr);
                sw1.Stop();
                totalTime1 += sw1.ElapsedTicks;
            }
            double avgTime1 = (double)totalTime1 / runs1 / Stopwatch.Frequency * 1000;
            
            long totalTime2 = 0;
            int runs2 = 20;
            int result2 = 0;
            
            for (int i = 0; i < runs2; i++)
            {
                Stopwatch sw2 = Stopwatch.StartNew();
                result2 = FindMaxSubarrayImproved(arr);
                sw2.Stop();
                totalTime2 += sw2.ElapsedTicks;
            }
            double avgTime2 = (double)totalTime2 / runs2 / Stopwatch.Frequency * 1000;
            
            if (result1 != result2)
            {
                Console.WriteLine("Ошибка: результаты не совпадают!");
                return;
            }
            
            double speedup = avgTime1 / avgTime2;
            Console.WriteLine($"{size}\t{avgTime1:F3}\t\t{avgTime2:F3}\t\t{speedup:F1}x");
        }
    }
    
    static int[] GenerateArray(int size)
    {
        Random rand = new Random();
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = rand.Next(-100, 100);
        }
        return arr;
    }
    
    // Оригинальный алгоритм O(n^3)
    static int FindMaxSubarray(int[] arr)
    {
        int max = int.MinValue;
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i; j < arr.Length; j++)
            {
                int sum = 0;
                for (int k = i; k <= j; k++)
                {
                    sum += arr[k];
                }
                if (sum > max) max = sum;
            }
        }
        return max;
    }
    
    // Улучшенный алгоритм O(n^2)
    static int FindMaxSubarrayImproved(int[] arr)
    {
        int max = int.MinValue;
        
        for (int i = 0; i < arr.Length; i++)
        {
            int currentSum = 0;
            for (int j = i; j < arr.Length; j++)
            {
                currentSum += arr[j];
                if (currentSum > max)
                    max = currentSum;
            }
        }
        
        return max;
    }
}