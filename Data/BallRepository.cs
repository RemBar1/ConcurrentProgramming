using ConcurrentProgramming.PresentationModel;
using System.Collections.ObjectModel;

namespace ConcurrentProgramming.Data
{
    public class BallRepository
    {
        public ObservableCollection<Ball> Balls { get; private set; }

        public BallRepository()
        {
            Balls = new ObservableCollection<Ball>();
        }

        public void AddBall(int positionX, int positionY)
        {
            Balls.Add(new Ball(positionX, positionY));
        }
    }
}
