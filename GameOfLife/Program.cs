using IvorChalton.GameOfLife.View;
using System;

namespace IvorChalton.GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var view = new ConsoleView(40, 100);
                view.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}
