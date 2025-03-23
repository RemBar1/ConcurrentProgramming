using ConcurrentProgramming.PresentationModel;
using System.Collections.ObjectModel;

namespace ConcurrentProgramming.Data
{
    public class BallRepository
    {
        private ObservableCollection<Ball> balls;
        public BallRepository()
        {
            this.balls = [];
        }
        public ObservableCollection<Ball> Balls { get => balls; set => balls = value; }
        public void AddBall(int positionX, int positionY)
        {
            balls.Add(new Ball(positionX, positionY));
        }
    }
}
