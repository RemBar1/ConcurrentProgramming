using System.ComponentModel;

namespace ConcurrentProgramming.Model
{
    public interface IBall : INotifyPropertyChanged
    {
        int PositionX { get; }
        int PositionY { get; }

        event PropertyChangedEventHandler PropertyChanged;

        void Move();
    }
}
