using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task3
{
    class CustomSynchronizationContext : SynchronizationContext
    {
        public override void Post(SendOrPostCallback callBack, object state)
        {
            Thread thread = new Thread(() => callBack(state));
            thread.Name = "CustomThread";
            thread.Start();
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            SynchronizationContext.SetSynchronizationContext(new CustomSynchronizationContext());

            Console.WriteLine("Before the async method:");
            Console.WriteLine($"ID of the thead: {Thread.CurrentThread.ManagedThreadId}\n" +
                              $"Name of the thread: {Thread.CurrentThread.Name}\n" +
                              $"If thread is from pool: {Thread.CurrentThread.IsThreadPoolThread}\n" +
                              new string('-', 75));

            await FactorialAsync(5).ConfigureAwait(false);


            Console.WriteLine("After the async method:");

            Console.WriteLine($"ID of the thead (after the calculations are completed): {Thread.CurrentThread.ManagedThreadId}\n" +
                  $"Name of the thread (after the calculations are completed): {Thread.CurrentThread.Name}\n" +
                  $"If thread is from pool (after the calculations are completed): {Thread.CurrentThread.IsThreadPoolThread}");
        }

        static async Task FactorialAsync(int num)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"ID of the thead (during calculating): {Thread.CurrentThread.ManagedThreadId}\n" +
                                  $"Name of the thread (during calculating): {Thread.CurrentThread.Name}\n" +
                                  $"If thread is from pool (during calculating): {Thread.CurrentThread.IsThreadPoolThread}\n" +
                                  new string('-', 75));

                int result = 1;
                for (int i = 1; i <= num; i++)
                {
                    result *= i;
                }
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Factorial of a number {num} = {result}");
                Console.ResetColor();
            }).ConfigureAwait(false);


        }
    }

}