using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;

namespace ConcurrentProgramming.Data
{
    public interface IBallRepository
    {
        void Add(IBall ball);
        void Clear();
        IReadOnlyList<IBall> GetAll();
        int Count { get; }
        ObservableCollection<IBall> Balls { get; }
    }
}