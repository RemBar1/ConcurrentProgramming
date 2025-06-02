using ConcurrentProgramming.Data;
using ConcurrentProgramming.Logic.Collision;
using ConcurrentProgramming.Logic.Physics;
using ConcurrentProgramming.Model;
using System.Diagnostics;
using System.Text;

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
        private readonly Task? simulationTask;
        private bool disposed;
        private const double TargetFrameTime = 16.0;
        private const double FixedTimeStep = 1.0 / 60.0;
        private readonly List<Thread> simulationThreads = [];
        private readonly ILogger logger;
        private readonly string logFilePath = "ball_diagnostics.log";
        private readonly StringBuilder logBuffer = new();
        private const int MaxBufferSize = 4096; // 4KB

        public BallService(IBallRepository repository, int boardWidth, int boardHeight, int boardThickness)
        {
            ballRepository = repository;
            actualBoardWidth = boardWidth - (boardThickness * 2);
            actualBoardHeight = boardHeight - (boardThickness * 2);
            ballPhysics = new PhysicsEngine();
            collisionDetector = new CollisionDetector();
            ClearLogFile();
            logger = new FileLogger(logFilePath);

        }

        public bool IsSimulationRunning => simulationTask != null && !simulationTask.IsCompleted;

        public void CreateBalls(int count, int diameter)
        {
            Random random = new();

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
                    Velocity = new Vector2((random.NextDouble() * 100) - 50, (random.NextDouble() * 100) - 50)
                };
                ballRepository.Add(ball);
            }
        }

        private void HandleCollisions()
        {
            IReadOnlyList<IBall> balls = ballRepository.GetAll();

            foreach (IBall ball in balls)
            {
                if (ballPhysics.HandleWallCollision(ball, actualBoardWidth, actualBoardHeight))
                {
                    LogBallData(ball, "WALL_COLLISION");
                }
            }

            foreach ((IBall a, IBall b) in collisionDetector.DetectCollisions(balls))
            {
                if (ballPhysics.HandleBallCollision(a, b))
                {
                    LogBallData(a, $"BALL_COLLISION_WITH:{b.Id}");
                    LogBallData(b, $"BALL_COLLISION_WITH:{a.Id}");
                }
            }
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            StopSimulation();
            FlushLogBuffer();
            cts?.Dispose();
            disposed = true;
        }

        public void StartSimulation()
        {
            if (simulationThreads.Any(t => t.IsAlive))
            {
                return;
            }

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
            if (cts != null)
            {
                cts.Cancel();
                foreach (Thread thread in simulationThreads)
                {
                    if (thread.IsAlive)
                    {
                        thread.Join();
                    }
                }
                ballRepository.Clear();
                simulationThreads.Clear();
            }
        }
        private void LogBallData(IBall ball, string collisionType)
        {
            try
            {
                string logEntry =
                 $"ID:{ball.Id.ToString().PadLeft(4)} | " +
                 $"{DateTime.Now:yyyy-MM-ddTHH:mm:ss.fff} | " +
                 $"Pos:X:{ball.Position.X.ToString("0.00").PadLeft(6)},Y:{ball.Position.Y.ToString("0.00").PadLeft(6)} | " +
                 $"Vel:X:{ball.Velocity.X.ToString("0.00").PadLeft(6)},Y:{ball.Velocity.Y.ToString("0.00").PadLeft(6)} | " +
                 $"Type:{collisionType.PadRight(10)}{Environment.NewLine}";
                lock (lockObject)
                {
                    _ = logBuffer.Append(logEntry);

                    if (logBuffer.Length >= MaxBufferSize)
                    {
                        FlushLogBuffer();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Błąd logowania: {ex.Message}");
            }
        }

        private void FlushLogBuffer()
        {
            try
            {
                lock (lockObject)
                {
                    if (logBuffer.Length > 0)
                    {
                        logger.Log(logBuffer.ToString());
                        _ = logBuffer.Clear();
                    }
                }
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"Błąd zapisu bufora logów: {ex.Message}");
            }
        }

        private void ClearLogFile()
        {
            try
            {
                File.WriteAllText(logFilePath, string.Empty);
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"Nie udało się wyczyścić pliku loga: {ex.Message}");
            }
        }
    }
}
