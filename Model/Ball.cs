using System.ComponentModel;
using System.Numerics;

namespace ConcurrentProgramming.Model
{
    public class Ball : IBall
    {
        private int positionX;
        private int positionY;
        private const int maxWidth = 700;
        private const int maxHeight = 500;
        private const int BallDiameter = 20;
        private VectorTo velocity;

        public VectorTo Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        public Ball(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.velocity = new VectorTo(1, 1);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public int PositionX
        {
            get => positionX;
            set
            {
                if (positionX != value)
                {
                    positionX = value;
                    OnPropertyChanged(nameof(PositionX));
                }
            }
        }

        public int PositionY
        {
            get => positionY;
            set
            {
                if (positionY != value)
                {
                    positionY = value;
                    OnPropertyChanged(nameof(PositionY));
                }
            }
        }

        public static int Diameter => BallDiameter;

        public static int MaxWidth => maxWidth;

        public static int MaxHeight => maxHeight;

        public void Move()
        {
            int newX = positionX + velocity.X;
            int newY = positionY + velocity.Y;

            if (newX < 0 || newX + BallDiameter > maxWidth)
            {
                velocity.X = -velocity.X;
                newX = Math.Clamp(newX, 0, maxWidth - BallDiameter);
            }
            if (newY < 0 || newY + BallDiameter > maxHeight)
            {
                velocity.Y = -velocity.Y;
                newY = Math.Clamp(newY, 0, maxHeight - BallDiameter);
            }

            PositionX = newX;
            PositionY = newY;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
