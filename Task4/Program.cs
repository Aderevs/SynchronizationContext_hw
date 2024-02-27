using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task4
{
    class CustomSynchronizationContext : SynchronizationContext
    {
        public override void Post(SendOrPostCallback callBack, object state)
        {
            try
            {
                Thread thread = new Thread(() => callBack(state));
                thread.Name = "CustomThread";
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown exception appeared:");
                Console.WriteLine(ex.Message);
            }
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            SynchronizationContext.SetSynchronizationContext(new CustomSynchronizationContext());

            FactorialAsync(5);
        }

        static async void FactorialAsync(int num)
        {
            await Task.Run(() =>
            {
                int result = 1;
                for (int i = 1; i <= num; i++)
                {
                    result *= i;
                }
                throw new InvalidOperationException("Random exception");
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Factorial of a number {num} = {result}");
                Console.ResetColor();
            });
        }
    }

}