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
            ArgumentNullException.ThrowIfNull(ball);
            balls.Add(ball);
        }

        public void Clear() => balls.Clear();

        public IReadOnlyList<IBall> GetAll() => balls;

        public int Count => balls.Count;
    }
}