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
                ball.Position = new Vector2(
                    Math.Clamp(ball.Position.X, 0, width - ball.Diameter),
                    ball.Position.Y);
            }

            if (ball.Position.Y <= 0 || ball.Position.Y >= height - ball.Diameter)
            {
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
                ball.Position = new Vector2(
                    ball.Position.X,
                    Math.Clamp(ball.Position.Y, 0, height - ball.Diameter));
            }
        }

        public void HandleBallCollision(IBall a, IBall b)
        {
            Vector2 delta = b.Position - a.Position;
            double distance = delta.Length;
            double minDistance = (a.Diameter + b.Diameter) / 2.0;

            if (distance < minDistance)
            {
                double overlap = minDistance - distance;
                Vector2 correction = delta.Normalized() * overlap * 0.5;

                a.Position -= correction;
                b.Position += correction;
            }

            Vector2 normal = delta.Normalized();
            Vector2 relativeVelocity = b.Velocity - a.Velocity;
            double velocityAlongNormal = relativeVelocity.Dot(normal);

            if (velocityAlongNormal > 0) return;

            double restitution = 1.0;
            double impulseMagnitude = -(1 + restitution) * velocityAlongNormal;
            impulseMagnitude /= (1 / a.Mass) + (1 / b.Mass);

            Vector2 impulse = normal * impulseMagnitude;

            a.Velocity -= impulse * (1 / a.Mass);
            b.Velocity += impulse * (1 / b.Mass);
        }
    }
}