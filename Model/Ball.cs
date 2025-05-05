using System.ComponentModel;

namespace ConcurrentProgramming.Model
{
    public class Ball : IBall, INotifyPropertyChanged
    {
        private int diameter;
        private Vector2 velocity;
        private Vector2 position;

        public Ball(int id, Vector2 position, int diameter)
        {
            this.Id = id;
            this.position = position;
            this.diameter = diameter;
        }

        public int Id { get; }

        public Vector2 Position
        {
            get => position;
            set
            {
                if (!position.Equals(value))
                {
                    position = value;
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
            set => velocity = value;
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