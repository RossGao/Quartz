using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var listA=new List<int> { 1,2,3,4,5,6};
            //var listB = new List<int> { 2 };

            //Console.WriteLine("A except B is:");
            //listA.Except(listB).ToList().ForEach(i => Console.Write($"{i}, "));

            //Console.WriteLine("B except A is:");
            //listB.Except(listA).ToList().ForEach(i => Console.Write($"{i}, "));

            var asyncTask = GetFileLengthAsync();
            Console.WriteLine($"Main. Time {DateTime.Now.Ticks}. Thread: {Thread.CurrentThread.ManagedThreadId}");

            asyncTask.Wait();
            Console.Read();
        }

        public static async Task<int> GetFileLengthAsync()
        {
            var filePath = @"D:\Books\VueJS\The Majesty Of Vue.js.pdf";
            using (var reader = new StreamReader(filePath))
            {
                Console.WriteLine($"Before await: Thread Id {Thread.CurrentThread.ManagedThreadId}");
                string v = await reader.ReadToEndAsync();

                int count = 0;
                for (int i = 0; i < 100000; i++)
                {
                    count += i;
                }

                Console.WriteLine($"GetFileLengthAsync. Thread: {Thread.CurrentThread.ManagedThreadId}");
                return v.Length;
            }

            //var name = await GetNameFromIO();
            //Console.WriteLine($"GetNameAsync. Time: {DateTime.Now.Ticks}. Thread: {Thread.CurrentThread.ManagedThreadId}");
            //return name;
        }

        public static Task<string> GetNameFromIO()
        {
            int count = 0;
            for(int i=0; i<100000;i++)
            {
                count += i;
            }

            Console.WriteLine($"GetNameFromIO. Time {DateTime.Now.Ticks}. Thread: {Thread.CurrentThread.ManagedThreadId}");
            return Task.FromResult("Ross");
        }
    }
}
