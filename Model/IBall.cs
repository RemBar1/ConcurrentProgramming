using System.ComponentModel;

namespace ConcurrentProgramming.Model
{
    public interface IBall : INotifyPropertyChanged
    {
        int PositionX { get; set; }
        int PositionY { get; set;}
        int Diameter { get; set; }
        VectorTo Velocity { get; set; }

        new event PropertyChangedEventHandler PropertyChanged;
    }
}
