using System.ComponentModel;

namespace ConcurrentProgramming.Model
{
    public class Ball : IBall, INotifyPropertyChanged
    {
        private int positionX;
        private int positionY;
        private readonly int boardWidth;
        private readonly int boardHeight;
        private const int BallDiameter = 20;
        private VectorTo velocity;

        public VectorTo Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        public Ball(int positionX, int positionY, int boardWidth, int boardHeight)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;
            velocity = new VectorTo(1, 1);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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

            if (newX < 0 || newX + BallDiameter > boardWidth)
            {
                velocity.X = -velocity.X;
                newX = Math.Clamp(newX, 0, boardWidth - BallDiameter);
            }
            if (newY < 0 || newY + BallDiameter > boardHeight)
            {
                velocity.Y = -velocity.Y;
                newY = Math.Clamp(newY, 0, boardHeight - BallDiameter);
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
