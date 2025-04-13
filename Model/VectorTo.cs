namespace ConcurrentProgramming.Model
{
    public struct VectorTo(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public readonly double Length => Math.Sqrt(X * X + Y * Y);
    }
}
