using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.Logic.Collision
{
    public class CollisionDetector : ICollisionDetector
    {
        public IEnumerable<(IBall, IBall)> DetectCollisions(IReadOnlyList<IBall> balls)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    if (IsColliding(balls[i], balls[j]))
                    {
                        yield return (balls[i], balls[j]);
                    }
                }
            }
        }

        private bool IsColliding(IBall a, IBall b)
        {
            double distance = (a.Position - b.Position).Length;
            return distance <= ((a.Diameter / 2) + (b.Diameter / 2));
        }
    }
}