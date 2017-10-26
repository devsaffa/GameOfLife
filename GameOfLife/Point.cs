namespace IvorChalton.GameOfLife
{
    /// <summary>
    /// A point in space
    /// </summary>
    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static int CalcHash(int x, int y)
        {
            // Ensure a unique hash using primes. 
            int hash = 23;
            hash = hash * 31 + x;
            hash = hash * 31 + y;
            return hash;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
