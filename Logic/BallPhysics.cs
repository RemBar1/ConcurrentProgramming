using ConcurrentProgramming.Data;

namespace ConcurrentProgramming.Logic
{
    public class BallPhysics : IBallPhysics
    {
        private readonly IBallRepository ballRepository;
        Random random = new();
        private CancellationTokenSource cts = new();

        public BallPhysics(IBallRepository repository) => ballRepository = repository;

        public void CreateBalls()
        {
            ballRepository.Clear();
            for (int i = 0; i < random.Next(5, 10); i++)
            {
                ballRepository.AddBall(random.Next(0, 600), random.Next(0, 400));
            }
        }

        public void StartSimulation()
        {
            cts = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    foreach (var ball in ballRepository.Balls)
                    {
                        ball.Move();
                    }
                    await Task.Delay(5, cts.Token);
                }
            }, cts.Token);
        }

        public void StopSimulation() => cts?.Cancel();
    }
}
