using IvorChalton.GameOfLife.DTO;
using System;
using System.Collections.Generic;

namespace IvorChalton.GameOfLife.Engine
{
    class CellsUpdatedEventArgs : EventArgs
    {
        public IEnumerable<Cell> Cells { get; set; }
    }
}
