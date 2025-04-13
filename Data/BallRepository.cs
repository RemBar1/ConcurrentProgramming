using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;

namespace ConcurrentProgramming.Data
{
    public class BallRepository : IBallRepository
    {
        private ObservableCollection<IBall> balls = [];

        public ObservableCollection<IBall> Balls { get => balls; set => balls = value; }

        public void Add(IBall ball)
        {
            balls.Add(ball);
        }

        public void Clear()
        {
            balls.Clear();
        }
    }
}
