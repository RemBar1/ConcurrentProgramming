using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.Logic.Collision
{
    public interface ICollisionDetector
    {
        IEnumerable<(IBall, IBall)> DetectCollisions(IReadOnlyList<IBall> balls);
    }
}
