using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;

namespace ConcurrentProgramming.Data
{
    public interface IBallRepository
    {
        ObservableCollection<IBall> Balls { get; set; }
        void Add(IBall ball);
        void Clear();
        IReadOnlyList<IBall> GetAll();
    }
}