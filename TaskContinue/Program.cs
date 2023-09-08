using System;
using System.Reflection;

namespace TaskContinue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 5,3,2,6,7,8,3,5,1};
            
            Task task1 = new Task(() => {
                Console.WriteLine("Task1");
                arr.Distinct().ToArray(); 
            });
            Task task2 = task1.ContinueWith(t => 
            {
                Console.WriteLine("Tas21");
                Array.Sort(arr); 
                return arr; 

            });

            Task task3 = task2.ContinueWith(t => 
            {
                Console.WriteLine("Task3");
                Console.WriteLine("Enter search number: ");
                int num = System.Convert.ToInt16(Console.ReadLine());
                int targetIndex = BinarySearch(arr,num);
                Console.WriteLine($"Бинарный поиск {num} вернул индекс {targetIndex}");
                
            });

            task1.Start();
            task3.Wait();
            

        }

        static int BinarySearch(int[] array, int target)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (array[mid] == target)
                {
                    return mid;
                }
                else if (array[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return -1; 
        }
    }
}