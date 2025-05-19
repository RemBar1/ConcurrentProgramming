using System.ComponentModel;

namespace ConcurrentProgramming.Model
{
    public interface IBall : INotifyPropertyChanged
    {
        Vector2 Position { get; set; }
        int Diameter { get; set; }
        double Mass { get; }
        Vector2 Velocity { get; set; }
        int Id { get; }

        void UpdatePosition(Vector2 newPosition);
    }
}