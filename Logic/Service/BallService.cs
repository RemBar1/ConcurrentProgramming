using ConcurrentProgramming.Data;
using ConcurrentProgramming.Logic.Collision;
using ConcurrentProgramming.Logic.Physics;
using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.Logic.Service
{
    public class BallService : IBallService
    {
        private readonly IBallRepository ballRepository;
        private readonly IPhysicsEngine ballPhysics;
        private readonly ICollisionDetector collisionDetector;
        private readonly int actualBoardWidth;
        private readonly int actualBoardHeight;
        private readonly object lockObject = new();
        private CancellationTokenSource cts = new();

        public BallService(IBallRepository repository, int boardWidth, int boardHeight, int boardThickness)
        {
            ballRepository = repository;
            actualBoardWidth = boardWidth - (boardThickness * 2);
            actualBoardHeight = boardHeight - (boardThickness * 2);
            ballPhysics = new PhysicsEngine();
            collisionDetector = new CollisionDetector();
        }

        public void CreateBalls(int count, int diameter)
        {
            Random random = new();

            for (int i = 0; i < count; i++)
            {
                Vector2 position;
                bool collision;

                do
                {
                    collision = false;
                    position = new Vector2(
                        random.Next(diameter, actualBoardWidth - diameter),
                        random.Next(diameter, actualBoardHeight - diameter));

                    foreach (IBall existingBall in ballRepository.Balls)
                    {
                        double distance = (position - existingBall.Position).Length;
                        if (distance < (diameter / 2) + (existingBall.Diameter / 2))
                        {
                            collision = true;
                            break;
                        }
                    }
                } while (collision);

                Ball ball = new(
                    id: i,
                    position: position,
                    diameter: diameter
                )
                {
                    Velocity = new Vector2((random.NextDouble() * 4) - 2, (random.NextDouble() * 4) - 2)
                };
                ballRepository.Add(ball);
            }
        }

        private void UpdateBalls()
        {
            foreach (IBall ball in ballRepository.GetAll())
            {
                Vector2 newPos = ball.Position + (ball.Velocity * 0.016);
                ball.UpdatePosition(newPos);
            }
        }

        private void HandleCollisions()
        {
            IReadOnlyList<IBall> balls = ballRepository.GetAll();

            foreach (IBall ball in balls)
            {
                ballPhysics.HandleWallCollision(ball, actualBoardWidth, actualBoardHeight);
            }

            foreach ((IBall a, IBall b) in collisionDetector.DetectCollisions(balls))
            {
                ballPhysics.HandleBallCollision(a, b);
            }
        }
        public void StartSimulation()
        {
            cts = new CancellationTokenSource();
            _ = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    lock (lockObject)
                    {
                        foreach (IBall? ball in ballRepository.Balls.ToList())
                        {
                            UpdateBalls();
                            HandleCollisions();
                        }
                    }
                    await Task.Delay(16, cts.Token);
                }
            }, cts.Token);
        }

        public void StopSimulation()
        {
            if (cts != null)
            {
                ballRepository.Clear();
                cts.Cancel();
            }
        }
    }
}
