using IvorChalton.GameOfLife.DTO;
using System;
using System.Collections.Generic;

namespace IvorChalton.GameOfLife.View
{
    /// <summary>
    /// A World View
    /// </summary>
    interface IWorldView
    {
        event EventHandler<EventArgs> OnGrowOlderOrdered;
        event EventHandler<SeedWorldEventArgs> OnSeedWorld;

        /// <summary>
        /// Update the view with 
        /// </summary>
        /// <param name="cells"></param>
        void Update(IEnumerable<Cell> cells);
    }
}
