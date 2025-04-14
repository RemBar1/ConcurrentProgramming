using ConcurrentProgramming.Data;
using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.Logic
{
    public class BallService : IBallService
    {
        private readonly IBallRepository ballRepository;
        private readonly int boardWidth;
        private readonly int boardHeight;
        private readonly object lockObject = new object();
        private CancellationTokenSource cts = new();

        public BallService(IBallRepository repository, int boardWidth, int boardHeight, int boardThickness)
        {
            ballRepository = repository;
            this.boardWidth = boardWidth - boardThickness;
            this.boardHeight = boardHeight - boardThickness;
        }

        public void AddBall(int ballPositionX, int ballPositionY, int diameter)
        {
            ballRepository.Add(new Ball(ballPositionX, ballPositionY, diameter));
        }

        public void MoveBall(IBall ball)
        {
            int newX = ball.PositionX + ball.Velocity.X;
            int newY = ball.PositionY + ball.Velocity.Y;

            if (newX < 0 || newX + ball.Diameter > boardWidth)
            {
                ball.Velocity = new VectorTo(-ball.Velocity.X, ball.Velocity.Y);
                newX = Math.Clamp(newX, 0, boardWidth - ball.Diameter);
            }

            if (newY < 0 || newY + ball.Diameter > boardHeight)
            {
                ball.Velocity = new VectorTo(ball.Velocity.X, -ball.Velocity.Y);
                newY = Math.Clamp(newY, 0, boardHeight - ball.Diameter);
            }

            ball.PositionX = newX;
            ball.PositionY = newY;
        }
        public void CreateBalls(int count, int diameter)
        {
            ballRepository.Clear();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                bool positionValid;
                int positionX, positionY;

                do
                {
                    positionX = random.Next(0, boardWidth - diameter);
                    positionY = random.Next(0, boardHeight - diameter);
                    positionValid = true;

                    foreach (var existingBall in ballRepository.Balls)
                    {
                        if (Math.Abs(existingBall.PositionX - positionX) < diameter &&
                            Math.Abs(existingBall.PositionY - positionY) < diameter)
                        {
                            positionValid = false;
                            break;
                        }
                    }

                } while (!positionValid);

                AddBall(positionX, positionY, diameter);
            }
        }

        public void StartSimulation()
        {
            cts = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    lock (lockObject)
                    {
                        foreach (var ball in ballRepository.Balls.ToList())
                        {
                            MoveBall(ball);
                        }
                    }
                    await Task.Delay(7, cts.Token);
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
