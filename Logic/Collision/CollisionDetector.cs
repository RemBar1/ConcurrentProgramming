using ConcurrentProgramming.Model;
using System.Collections.Concurrent;

namespace ConcurrentProgramming.Logic.Collision
{
    public class CollisionDetector : ICollisionDetector
    {
        public IEnumerable<(IBall, IBall)> DetectCollisions(IReadOnlyList<IBall> balls)
        {
            var results = new ConcurrentBag<(IBall, IBall)>();

            Parallel.For(0, balls.Count, i =>
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    if (IsColliding(balls[i], balls[j]))
                    {
                        results.Add((balls[i], balls[j]));
                    }
                }
            });

            return results;
        }

        private bool IsColliding(IBall a, IBall b)
        {
            double distance = (a.Position - b.Position).Length;
            return distance <= ((a.Diameter / 2) + (b.Diameter / 2));
        }
    }
}