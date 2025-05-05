using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.Logic.Physics
{
    public class PhysicsEngine : IPhysicsEngine
    {
        public void HandleWallCollision(IBall ball, int width, int height)
        {
            if (ball.Position.X <= 0 || ball.Position.X >= width - ball.Diameter)
            {
                ball.Velocity = new Vector2(-ball.Velocity.X, ball.Velocity.Y);
            }

            if (ball.Position.Y <= 0 || ball.Position.Y >= height - ball.Diameter)
            {
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
            }
        }

        public void HandleBallCollision(IBall a, IBall b)
        {
            Vector2 normal = (b.Position - a.Position).Normalized();
            Vector2 relativeVelocity = b.Velocity - a.Velocity;

            double impulse = 2 * relativeVelocity.Dot(normal) /
                           ((1 / a.Mass) + (1 / b.Mass));

            a.Velocity += normal * (impulse / a.Mass);
            b.Velocity -= normal * (impulse / b.Mass);
        }
    }
}