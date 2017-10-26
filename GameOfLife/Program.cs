using System;

namespace IvorChalton.GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            int worldSize = 50; // ...or pass from cmd line
            int seedSize = 200;

            var world = new World(worldSize);
            var acquirer = new SurroundingCellAcquirer(world.Cells);
            var being = new ConwayBeing(acquirer);
            world.Seed(seedSize, being);

            int iteration = 0;
            OutputWorld(world, 0);
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                world.GrowOlder();
                OutputWorld(world, ++iteration);
            }

            // perf testing
            //while (true)
            //{
            //    world.GrowOlder();
            //    Console.Write(".");
            //}
        }

        // TODO: I'd normally define a contract for a View, and pass that into an 'Engine', responsible for 'running' the world and calling Update() on the View implementation
        static void OutputWorld(World world, int iteration)
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"The Game of Life by Ivor Chalton. -- Iteration {iteration} -- ");
            Console.WriteLine("----------------------------------------------------------");

            for (int y = 0; y < world.MaxX; y++)
            {
                for (int x = 0; x < world.MaxY; x++)
                {
                    var cell = world.Cells[Point.CalcHash(x, y)];
                    Console.Write(cell != null ?
                        cell.IsAlive ? "1" : "0"
                        : "0");
                }

                Console.WriteLine();
            }
        }
    }

}
