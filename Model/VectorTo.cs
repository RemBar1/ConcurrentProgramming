namespace ConcurrentProgramming.Model
{
    public struct VectorTo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Length => Math.Sqrt(X * X + Y * Y);

        public VectorTo(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
