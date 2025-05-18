using ConcurrentProgramming.Data;
using ConcurrentProgramming.Logic.Collision;
using ConcurrentProgramming.Logic.Physics;
using ConcurrentProgramming.Model;
using System.Diagnostics;

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
        private Task? simulationTask;
        private bool disposed;
        private readonly Stopwatch frameStopwatch = new();
        private const double TargetFrameTime = 16.0;

        public BallService(IBallRepository repository, int boardWidth, int boardHeight, int boardThickness)
        {
            ballRepository = repository;
            actualBoardWidth = boardWidth - (boardThickness * 2);
            actualBoardHeight = boardHeight - (boardThickness * 2);
            ballPhysics = new PhysicsEngine();
            collisionDetector = new CollisionDetector();
        }

        public bool IsSimulationRunning => simulationTask != null && !simulationTask.IsCompleted;

        public void CreateBalls(int count, int diameter)
        {
            Random random = new();
            //if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
            //if (diameter <= 0) throw new ArgumentOutOfRangeException(nameof(diameter));

            ballRepository.Clear();

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

                    foreach (IBall existingBall in ballRepository.GetAll())
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

        private void UpdateBalls(double deltaTime)
        {
            foreach (IBall ball in ballRepository.GetAll())
            {
                Vector2 newPos = ball.Position + (ball.Velocity * deltaTime);
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
            if (simulationTask != null && !simulationTask.IsCompleted)
                return;

            cts = new CancellationTokenSource();
            simulationTask = Task.Run(async () =>
            {
                frameStopwatch.Start();
                double lastTime = frameStopwatch.ElapsedMilliseconds;

                while (!cts.Token.IsCancellationRequested)
                {
                    double currentTime = frameStopwatch.ElapsedMilliseconds;
                    double deltaTime = currentTime - lastTime;

                    if (deltaTime >= TargetFrameTime)
                    {
                        lock (lockObject)
                        {
                            UpdateBalls(deltaTime / 100.0); 
                            HandleCollisions();
                        }
                        lastTime = currentTime;
                    }
                    else
                    {
                        int sleepTime = (int)(TargetFrameTime - deltaTime);
                        await Task.Delay(Math.Max(1, sleepTime), cts.Token);
                    }
                }
            }, cts.Token);
        }

        public void Dispose()
        {
            if (disposed) return;
            StopSimulation();
            cts?.Dispose();
            disposed = true;
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
