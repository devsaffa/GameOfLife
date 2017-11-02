using IvorChalton.GameOfLife.DTO;
using IvorChalton.GameOfLife.Engine;
using IvorChalton.GameOfLife.Presenter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IvorChalton.GameOfLife.View
{
    /// <summary>
    /// Console implemenation of a World-view
    /// </summary>
    class ConsoleView : IWorldView
    {
        public event EventHandler<EventArgs> OnGrowOlderOrdered;
        public event EventHandler<SeedWorldEventArgs> OnSeedWorld;

        private readonly WorldPresenter presenter;
        private readonly World world;

        /// <summary>
        /// Create a console View. 
        /// </summary>
        /// <param name="worldSize"></param>
        /// <param name="seedSize"></param>
        public ConsoleView(int worldSize, int seedSize)
        {
            world = new World(worldSize);
            if (seedSize > (worldSize * worldSize))
                throw new InvalidOperationException("Seed size is too large for this world");

            if (worldSize > Console.LargestWindowHeight || worldSize > Console.LargestWindowWidth)
                throw new InvalidOperationException("World size is too big for a Console view");

            // Note: I don't really like that the world's cells are used directly in the cellAcquirer, but am sacrificing immutability for speed. TODO: Consider refactoring
            var acquirer = new SurroundingCellAcquirer(world.Cells);
            var being = new ConwayBeing(acquirer);
            world.Configure(being);

            presenter = new WorldPresenter(this, world);

            ConfigureConsole();

            // Seed the world
            OnSeedWorld?.Invoke(this, new SeedWorldEventArgs { NumCells = seedSize });

            // 
            Update(world.Cells.Select(c => c.Value).ToList());
        }

        void ConfigureConsole()
        {
            Console.Clear();
            Console.SetWindowSize(world.MaxX + 2, world.MaxY + 2);
            Console.SetBufferSize(world.MaxX + 2, world.MaxY + 2);
            Console.WindowLeft = 0;
            Console.WindowTop = 0;
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Manually update the cells in the console window using a raster scan.
        /// Note: This was just a sanity check when seeing issues with SetCursorPosition() mechanism
        /// </summary>
        void UpdateManually()
        {
            Console.Clear();

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

        /// <summary>
        /// Main Console loop waiting for keypresses to effect world-aging
        /// </summary>
        public void Run()
        {
            ConsoleKey key;
            while ((key = Console.ReadKey().Key) != ConsoleKey.Escape)
            {
                switch (key)
                {
                    case ConsoleKey.Add:
                        OnSeedWorld?.Invoke(this, new SeedWorldEventArgs { NumCells = 1 });
                        break;

                    case ConsoleKey.NumPad0:
                        UpdateManually();
                        break;

                    default:
                        OnGrowOlderOrdered(this, EventArgs.Empty);
                        break;
                }

                Console.SetCursorPosition(0, world.MaxY);
            }
        }

        public void Update(IEnumerable<Cell> cells)
        {
            Debug.WriteLine($"{cells.Count()} cells are being updated");

            foreach (var cell in cells)
            {
                // NOTE: Seems like SetCursorPosition becomes flaky when the World size gets big (i.e. when the console buffer increases). Have not investigated further.
                //       The cursor position seems to rotate around some sort of buffer and x&y positions are not set to what is requested -- even when I've increased the buffer and size manually
                Console.SetCursorPosition(cell.Location.X, cell.Location.Y);
                Console.Write(cell.IsAlive ? "1" : "0");
            }
        }
    }
}

