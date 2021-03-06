﻿using IvorChalton.GameOfLife.DTO;
using System.Collections.Generic;

namespace IvorChalton.GameOfLife.Engine
{
    /// <summary>
    /// Acquires Cells relative to a specific point, from a Dictionary of points
    /// </summary>
    interface ICellAcquirer
    {
        List<Cell> Acquire(Point p);
    }
}
