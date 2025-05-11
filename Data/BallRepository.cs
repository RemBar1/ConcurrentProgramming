using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;

namespace ConcurrentProgramming.Data
{
    public class BallRepository : IBallRepository
    {
        private readonly ObservableCollection<IBall> balls = new();
        public ObservableCollection<IBall> Balls => balls;

        public void Add(IBall ball)
        {
            if (ball == null) throw new ArgumentNullException(nameof(ball));
            balls.Add(ball);
        }

        public void Clear() => balls.Clear();

        public IReadOnlyList<IBall> GetAll() => balls;

        public int Count => balls.Count;
    }
}