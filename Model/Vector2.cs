namespace ConcurrentProgramming.Model
{
    public readonly struct Vector2 : IEquatable<Vector2>
    {
        public double X { get; }
        public double Y { get; }
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

        public bool Equals(Vector2 other) => X.Equals(other.X) && Y.Equals(other.Y);
        public override bool Equals(object obj) => obj is Vector2 other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(Vector2 left, Vector2 right) => left.Equals(right);
        public static bool operator !=(Vector2 left, Vector2 right) => !left.Equals(right);

        public override string ToString()
        {
            return $"X:{X:0.00},Y:{Y:0.00}";
        }
    }
}
