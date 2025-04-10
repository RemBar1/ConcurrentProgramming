using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;

namespace ConcurrentProgramming.Data
{
    public class BallRepository : IBallRepository
    {
        private ObservableCollection<IBall> balls = new();

        public ObservableCollection<IBall> Balls { get => balls; set => balls = value; }

        public void AddBall(int x, int y)
        {
            IBall ball = new Ball(x, y);
            balls.Add(ball);
        }

        public void Clear()
        {
            balls.Clear();
        }
    }
}
