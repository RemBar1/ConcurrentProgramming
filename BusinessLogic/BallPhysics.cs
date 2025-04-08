using ConcurrentProgramming.Data;
using ConcurrentProgramming.PresentationModel;

namespace ConcurrentProgramming.BusinessLogic
{
    public class BallPhysics
    {
        private BallRepository ballRepository;
        Random random = new();
        private CancellationTokenSource cancellationTokenSource = new();

        public BallPhysics(BallRepository ballRepository)
        {
            this.BallRepository = ballRepository;
        }

        public BallRepository BallRepository { get => ballRepository; set => ballRepository = value; }

        public void AddBall(int positionX, int positionY)
        {
            ballRepository.Balls.Add(new Ball(positionX, positionY));
        }

        public void CreateBalls()
        {
            BallRepository.Balls.Clear();
            for (int i = 0; i < random.Next(5, 10); i++)
            {
                AddBall(random.Next(0, 600), random.Next(0, 400));
            }
        }

        public void StartSimulation()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    foreach (var ball in BallRepository.Balls)
                    {
                        ball.Move();
                    }
                    await Task.Delay(5);
                }
            }, cancellationTokenSource.Token);
        }

        public void StopSimulation()
        {
            cancellationTokenSource.Cancel();
        }
    }
}
