using IvorChalton.GameOfLife.DTO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IvorChalton.GameOfLife.Engine
{
    /// <summary>
    /// The world in which cells live and die
    /// </summary>
    class World
    {
        /// <summary>
        /// Fired when the state of any cells have changed
        /// </summary>
        public event EventHandler<CellsUpdatedEventArgs> CellsUpdated;

        /// <summary>
        /// The Cells in this world
        /// </summary>
        /// <remarks>
        /// For speed, I've used a custom hash-creation using primes as the index (see Point.CalcHash()), rather than implementing IEquatable on a Point
        /// </remarks>
        public ConcurrentDictionary<int, Cell> Cells { get; private set; } = new ConcurrentDictionary<int, Cell>();

        /// <summary>
        /// The number of columns in this world: NOTE: I haven't built in automatic world-expansion, but this should be _fairly_ trivial
        /// </summary>
        public int MaxX { get; private set; }

        /// <summary>
        /// The number of rows in this world
        /// </summary>
        public int MaxY { get; private set; }


        IOmnipotentBeing _rulesGiver;

        public World(int size)
        {
            // Create a 'square' world and populate with dead cells using a concurrent raster scan
            ConcurrentBag<Cell> c = new ConcurrentBag<Cell>();
            Parallel.For(0, size, x => Parallel.For(0, size, y => c.Add(new Cell(x, y))));
            Parallel.ForEach(c, item => Cells.TryAdd(Point.CalcHash(item.Location.X, item.Location.Y), item));
            MaxX = size;
            MaxY = size;
        }

        public void Configure(IOmnipotentBeing being)
        {
            _rulesGiver = being;
        }

        /// <summary>
        /// Randomly seed the world with the supplied number of living cells
        /// </summary>
        /// <param name="numLiving">The number of living cells</param>
        /// <remarks>
        /// NOTE: May result in less actual living cells if cells are randomly chosen multiple times. 
        /// </remarks>
        public void Seed(int numLiving)
        {
            var cells = new List<Cell>();
            var rand = new Random();
            for (int i = 0; i < numLiving; i++)
            {
                int xPos = rand.Next(0, MaxX - 1);
                int yPos = rand.Next(0, MaxY - 1);

                var cell = Cells[Point.CalcHash(xPos, yPos)];
                cell.IsAlive = true;

                cells.Add(cell);
            }

            CellsUpdated?.Invoke(this, new CellsUpdatedEventArgs { Cells = cells });
        }

        /// <summary>
        /// Grow one iteration older
        /// </summary>
        public void GrowOlder()
        {
            if (_rulesGiver == null)
                throw new InvalidOperationException("World has not yet been configured with a rules-giver");

            var cellsToChange = new ConcurrentBag<Cell>();

            // Because all cell change instantaneously, first get the list of all cells that WILL change, using the current state
            Parallel.ForEach(Cells, cell =>
            {
                if (_rulesGiver.Decide(cell.Value))
                    cellsToChange.Add(cell.Value);
            });

            // Now update the cells
            Parallel.ForEach(cellsToChange, cell => cell.IsAlive = !cell.IsAlive);

            if (cellsToChange.Count > 0)
                CellsUpdated?.Invoke(this, new CellsUpdatedEventArgs {
                    Cells = cellsToChange.AsEnumerable()
                });
        }
    }
}
