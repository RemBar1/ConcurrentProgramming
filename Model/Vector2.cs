namespace ConcurrentProgramming.Model
{
    public struct Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Length => Math.Sqrt((X * X) + (Y * Y));

        public Vector2(double x, double y) { X = x; Y = y; }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 v, double scalar)
        {
            return new Vector2(v.X * scalar, v.Y * scalar);
        }

        public Vector2 Normalized()
        {
            return this * (1 / Length);
        }

        public double Dot(Vector2 other)
        {
            return (X * other.X) + (Y * other.Y);
        }
    }
}
