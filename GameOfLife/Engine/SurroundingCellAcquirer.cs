using IvorChalton.GameOfLife.DTO;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace IvorChalton.GameOfLife.Engine
{
    class SurroundingCellAcquirer : ICellAcquirer
    {
        readonly ConcurrentDictionary<int, Cell> _allCells;

        public SurroundingCellAcquirer(ConcurrentDictionary<int, Cell> allCells)
        {
            _allCells = allCells;
        }

        public List<Cell> Acquire(Point p)
        {
            List<Cell> cells = new List<Cell>();
            Cell c;
            if (_allCells.TryGetValue(Point.CalcHash(p.X - 1, p.Y + 1), out c))
                cells.Add(c);
            if (_allCells.TryGetValue(Point.CalcHash(p.X, p.Y + 1), out c))
                cells.Add(c);
            if (_allCells.TryGetValue(Point.CalcHash(p.X + 1, p.Y + 1), out c))
                cells.Add(c);
            if (_allCells.TryGetValue(Point.CalcHash(p.X - 1, p.Y), out c))
                cells.Add(c);
            if (_allCells.TryGetValue(Point.CalcHash(p.X + 1, p.Y), out c))
                cells.Add(c);
            if (_allCells.TryGetValue(Point.CalcHash(p.X - 1, p.Y - 1), out c))
                cells.Add(c);
            if (_allCells.TryGetValue(Point.CalcHash(p.X, p.Y - 1), out c))
                cells.Add(c);
            if (_allCells.TryGetValue(Point.CalcHash(p.X + 1, p.Y - 1), out c))
                cells.Add(c);

            return cells;
        }
    }
}
