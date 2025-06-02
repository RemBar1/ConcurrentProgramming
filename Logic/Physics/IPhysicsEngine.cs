using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.Logic.Physics
{
    public interface IPhysicsEngine
    {
        bool HandleWallCollision(IBall ball, int width, int height);
        bool HandleBallCollision(IBall a, IBall b);
    }
}
