namespace IvorChalton.GameOfLife.DTO
{
    /// <summary>
    /// A cell located at a point in space
    /// </summary>
    class Cell
    {
        /// <summary>
        /// This cell's location in space
        /// </summary>
        public Point Location { get; private set; }

        /// <summary>
        /// Indicates whether the cell is alive or not
        /// </summary>
        public bool IsAlive { get; set; }

        public Cell(int x, int y)
        {
            Location = new Point(x, y);
        }
    }
}
