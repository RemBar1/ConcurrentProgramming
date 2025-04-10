using System.ComponentModel;
using System.Numerics;

namespace ConcurrentProgramming.Model
{
    public class Ball : IBall
    {
        private int positionX;
        private int positionY;
        private const int MaxWidth = 700;
        private const int MaxHeight = 500;
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

        public void Move()
        {
            int newX = positionX + velocity.X;
            int newY = positionY + velocity.Y;

            if (newX < 0 || newX + BallDiameter > MaxWidth)
            {
                velocity.X = -velocity.X;
                newX = Math.Clamp(newX, 0, MaxWidth - BallDiameter);
            }
            if (newY < 0 || newY + BallDiameter > MaxHeight)
            {
                velocity.Y = -velocity.Y;
                newY = Math.Clamp(newY, 0, MaxHeight - BallDiameter);
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
