using ConcurrentProgramming.Data;

namespace ConcurrentProgramming.BusinessLogic
{
    public class BallPhysics
    {
        BallRepository ballRepository;
        Random random = new Random();
        private CancellationTokenSource cancellationTokenSource = new();

        public BallPhysics(BallRepository ballRepository)
        {
            this.ballRepository = ballRepository;
        }
        public void CreateBalls()
        {
            ballRepository.Balls.Clear();
            for (int i = 0; i < random.Next(5, 10); i++)
            {
                ballRepository.AddBall(random.Next(0, 600), random.Next(0, 400));
            }
        }

        public void StartSimulation()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    foreach (var ball in ballRepository.Balls)
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
