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

        public void AddBall(int ballPositionX, int ballPositionY, int ballBoardWidth, int ballBoardHeight)
        {
            ballRepository.Add(new Ball(ballPositionX, ballPositionY, ballBoardWidth, ballBoardHeight));
        }
        public void CreateBalls(int count)
        {
            ballRepository.Clear();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                bool positionValid;
                int positionX, positionY;

                do
                {
                    positionX = random.Next(0, boardWidth - Ball.Diameter);
                    positionY = random.Next(0, boardHeight - Ball.Diameter);
                    positionValid = true;

                    foreach (var existingBall in ballRepository.Balls)
                    {
                        if (Math.Abs(existingBall.PositionX - positionX) < Ball.Diameter &&
                            Math.Abs(existingBall.PositionY - positionY) < Ball.Diameter)
                        {
                            positionValid = false;
                            break;
                        }
                    }

                } while (!positionValid);

                AddBall(positionX, positionY, boardWidth, boardHeight);
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
                            ball.Move();
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
