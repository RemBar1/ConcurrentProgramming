using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;

namespace ConcurrentProgramming.Data
{
    public class BallRepository : IBallRepository
    {
        public ObservableCollection<IBall> Balls { get; set; } = [];

        public void Add(IBall ball)
        {
            Balls.Add(ball);
        }

        public void Clear()
        {
            Balls.Clear();
        }

        public IReadOnlyList<IBall> GetAll()
        {
            return Balls.ToList();
        }
    }
}