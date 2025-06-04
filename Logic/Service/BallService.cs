using ConcurrentProgramming.Data;
using ConcurrentProgramming.Logic.Collision;
using ConcurrentProgramming.Logic.Physics;
using ConcurrentProgramming.Model;
using System.Diagnostics;

namespace ConcurrentProgramming.Logic.Service
{
    public class BallService : IBallService, IDisposable
    {
        private readonly IBallRepository ballRepository;
        private readonly IPhysicsEngine ballPhysics;
        private readonly ICollisionDetector collisionDetector;
        private readonly int actualBoardWidth;
        private readonly int actualBoardHeight;
        private readonly object lockObject = new();
        private CancellationTokenSource cts = new();
        private readonly List<Thread> simulationThreads = new();
        private bool disposed;
        private const double TargetFrameTime = 16.0;
        private const double FixedTimeStep = 1.0 / 60.0;

        private const string LogsDirectory = "logs";
        private readonly string creationLogPath = Path.Combine(LogsDirectory, "ball_creation.log");
        private readonly string ballCollisionLogPath = Path.Combine(LogsDirectory, "ball_collisions.log");
        private readonly string wallCollisionLogPath = Path.Combine(LogsDirectory, "wall_collisions.log");
        private readonly string movementLogPath = Path.Combine(LogsDirectory, "ball_movement.log");

        private readonly ILogger creationLogger;
        private readonly ILogger ballCollisionLogger;
        private readonly ILogger wallCollisionLogger;
        private readonly ILogger movementLogger;
        private readonly Timer movementLogTimer;
        private readonly Timer collisionLogTimer;
        private const int LogIntervalMs = 100;

        public BallService(IBallRepository repository, int boardWidth, int boardHeight, int boardThickness)
        {
            ballRepository = repository;
            actualBoardWidth = boardWidth - (boardThickness * 2);
            actualBoardHeight = boardHeight - (boardThickness * 2);
            ballPhysics = new PhysicsEngine();
            collisionDetector = new CollisionDetector();

            InitializeLogFiles();

            creationLogger = new FileLogger(creationLogPath);
            ballCollisionLogger = new FileLogger(ballCollisionLogPath);
            wallCollisionLogger = new FileLogger(wallCollisionLogPath);
            movementLogger = new FileLogger(movementLogPath);
        }

        private void InitializeLogFiles()
        {
            try
            {
                Directory.CreateDirectory(LogsDirectory);

                File.WriteAllText(creationLogPath, string.Empty);
                File.WriteAllText(ballCollisionLogPath, string.Empty);
                File.WriteAllText(wallCollisionLogPath, string.Empty);
                File.WriteAllText(movementLogPath, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to initialize log files", ex);
            }
        }

        public void CreateBalls(int count, int diameter)
        {
            lock (lockObject)
            {
                Random random = new();
                ballRepository.Clear();

                creationLogger.Log($"=== CREATING {count} BALLS AT {DateTime.Now:yyyy-MM-ddTHH:mm:ss.fff} ===");

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
                        diameter: diameter)
                    {
                        Velocity = new Vector2((random.NextDouble() * 100) - 50, (random.NextDouble() * 100) - 50)
                    };
                    ballRepository.Add(ball);

                    creationLogger.Log(
                        $"ID:{ball.Id} | " +
                        $"Pos:X:{ball.Position.X:0.00},Y:{ball.Position.Y:0.00} | " +
                        $"Vel:X:{ball.Velocity.X:0.00},Y:{ball.Velocity.Y:0.00} | " +
                        $"Diameter:{ball.Diameter}");
                }
            }
        }

        public void StartSimulation()
        {

            cts = new CancellationTokenSource();
            simulationThreads.Clear();

            foreach (IBall ball in ballRepository.GetAll())
            {
                Thread ballThread = new(() =>
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    double previousTime = stopwatch.ElapsedMilliseconds;
                    double accumulatedTime = 0;

                    while (!cts.Token.IsCancellationRequested)
                    {
                        double currentTime = stopwatch.ElapsedMilliseconds;
                        double deltaTime = currentTime - previousTime;
                        previousTime = currentTime;

                        accumulatedTime += deltaTime;

                        if (accumulatedTime >= TargetFrameTime)
                        {
                            lock (lockObject)
                            {
                                Vector2 newPos = ball.Position + (ball.Velocity * FixedTimeStep);
                                ball.UpdatePosition(newPos);
                                LogMovementCallback(ball);
                                HandleCollisions();
                            }
                            accumulatedTime -= TargetFrameTime;
                        }

                        int sleepTime = (int)(TargetFrameTime - accumulatedTime);
                        if (sleepTime > 0)
                        {
                            Thread.Sleep(sleepTime);
                        }
                    }
                })
                {
                    IsBackground = true
                };
                simulationThreads.Add(ballThread);
                ballThread.Start();
            }
        }

        public void StopSimulation()
        {
            cts?.Cancel();

            foreach (Thread thread in simulationThreads)
            {
                if (thread.IsAlive)
                {
                    thread.Join();
                }
            }
            simulationThreads.Clear();
            ballRepository.Clear();
        }

        private void HandleCollisions()
        {
            IReadOnlyList<IBall> balls = ballRepository.GetAll();

            foreach (IBall ball in balls)
            {
                if (ballPhysics.HandleWallCollision(ball, actualBoardWidth, actualBoardHeight))
                {
                    LogWallCollision(ball);
                }
            }

            foreach ((IBall a, IBall b) in collisionDetector.DetectCollisions(balls))
            {
                if (ballPhysics.HandleBallCollision(a, b))
                {
                    LogBallCollision(a, b);
                }
            }
        }

        private void LogMovementCallback(IBall ball)
        {
            movementLogger.Log(
                $"{DateTime.Now:yyyy-MM-ddTHH:mm:ss.fff} | " +
                $"ID:{ball.Id} | " +
                $"Pos:X:{ball.Position.X:0.00},Y:{ball.Position.Y:0.00} | " +
                $"Vel:X:{ball.Velocity.X:0.00},Y:{ball.Velocity.Y:0.00}");

        }

        private void LogBallCollision(IBall ball1, IBall ball2)
        {
            ballCollisionLogger.Log(
                $"{DateTime.Now:yyyy-MM-ddTHH:mm:ss.fff} | " +
                $"BALLS:{ball1.Id} & {ball2.Id} | " +
                $"POS1:X:{ball1.Position.X:0.00},Y:{ball1.Position.Y:0.00} | " +
                $"POS2:X:{ball2.Position.X:0.00},Y:{ball2.Position.Y:0.00} | " +
                $"VEL1:X:{ball1.Velocity.X:0.00},Y:{ball1.Velocity.Y:0.00} | " +
                $"VEL2:X:{ball2.Velocity.X:0.00},Y:{ball2.Velocity.Y:0.00}");
        }

        private void LogWallCollision(IBall ball)
        {
            wallCollisionLogger.Log(
                $"{DateTime.Now:yyyy-MM-ddTHH:mm:ss.fff} | " +
                $"BALL:{ball.Id} | " +
                $"POS:X:{ball.Position.X:0.00},Y:{ball.Position.Y:0.00} | " +
                $"VEL:X:{ball.Velocity.X:0.00},Y:{ball.Velocity.Y:0.00} | " +
                $"WALL:{(ball.Position.X <= ball.Diameter / 2 || ball.Position.X >= actualBoardWidth - ball.Diameter / 2 ? "VERTICAL" : "HORIZONTAL")}");
        }

        public void Dispose()
        {
            if (disposed) return;

            StopSimulation();

            movementLogTimer?.Dispose();
            collisionLogTimer?.Dispose();
            cts?.Dispose();

            disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}