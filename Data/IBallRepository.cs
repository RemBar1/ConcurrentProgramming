using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;

namespace ConcurrentProgramming.Data
{
    public interface IBallRepository
    {
        ObservableCollection<IBall> Balls { get; }
        void AddBall(int x, int y);
        void Clear();
    }
}
