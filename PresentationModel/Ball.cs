using System.ComponentModel;

namespace ConcurrentProgramming.PresentationModel
{
    public class Ball
    {
        private int positionX;
        private int positionY;
        private int velocityX;
        private int velocityY;
        private const int MaxWidth = 700;
        private const int MaxHeight = 500;
        private const int BallDiameter = 20;

        public event PropertyChangedEventHandler PropertyChanged;

        public Ball(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.velocityX = 5;
            this.velocityY = 5;
        }

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

        public int Diameter => BallDiameter;
        public void Move()
        {
            int newX = positionX + velocityX;
            int newY = positionY + velocityY;

            if (newX < 0 || newX + BallDiameter > MaxWidth)
            {
                velocityX = -velocityX;
            }
            if (newY < 0 || newY + BallDiameter > MaxHeight)
            {
                velocityY = -velocityY;
            }

            PositionX += velocityX;
            PositionY += velocityY;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
