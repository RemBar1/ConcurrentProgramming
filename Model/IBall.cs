using System.ComponentModel;
using System.Drawing;

namespace ConcurrentProgramming.Model
{
    public interface IBall : INotifyPropertyChanged
    {
        Vector2 Position { get; set; }
        int Diameter { get; set; }
        double Mass { get; }
        Vector2 Velocity { get; set; }
        int Id { get; }
        Color Color { get; set; }

        void UpdatePosition(Vector2 newPosition);
    }
}