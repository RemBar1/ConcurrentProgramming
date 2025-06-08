using System.ComponentModel;
using System.Drawing;

namespace ConcurrentProgramming.Model
{
    public class Ball : IBall, INotifyPropertyChanged
    {
        private int diameter;
        private Vector2 velocity;
        private Vector2 position;
        private Color color;
        private readonly object locked = new();

        public Ball(int id, Vector2 position, int diameter)
        {
            this.Id = id;
            this.position = position;
            this.diameter = diameter;
            this.color = Color.Blue; // Default color
        }

        public int Id { get; }

        public Vector2 Position
        {
            get { lock (locked) return position; }
            set
            {
                if (!position.Equals(value))
                {
                    lock (locked) position = value;
                    OnPropertyChanged(nameof(Position));
                }
            }
        }

        public int Diameter
        {
            get => diameter;
            set
            {
                if (diameter != value)
                {
                    diameter = value;
                    OnPropertyChanged(nameof(Diameter));
                }
            }
        }

        public double Mass => diameter * diameter;

        public Vector2 Velocity
        {
            get => velocity;
            set
            {
                if (!velocity.Equals(value))
                {
                    velocity = value;
                    OnPropertyChanged(nameof(Velocity));
                }
            }
        }

        public Color Color
        {
            get => color;
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}