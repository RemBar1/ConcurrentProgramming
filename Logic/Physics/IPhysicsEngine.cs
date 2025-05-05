using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.Logic.Physics
{
    public interface IPhysicsEngine
    {
        void HandleWallCollision(IBall ball, int width, int height);
        void HandleBallCollision(IBall a, IBall b);
    }
}
