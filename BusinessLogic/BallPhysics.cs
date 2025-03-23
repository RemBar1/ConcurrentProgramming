using ConcurrentProgramming.Data;
using ConcurrentProgramming.PresentationModel;

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
            for (int i = 0; i < random.Next(5, 10); i++)
            {
                ballRepository.AddBall(random.Next(20, 380), random.Next(20, 400));
            }
        }

        public void StartSimulation()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    foreach (Ball ball in ballRepository.Balls)
                    {
                        ball.Move();
                    }
                    await Task.Delay(30);
                }
            }, cancellationTokenSource.Token);
        }

        public void StopSimulation()
        {
            cancellationTokenSource.Cancel();
        }
    }
}
