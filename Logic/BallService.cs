using ConcurrentProgramming.Data;
using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.Logic
{
    public class BallService : IBallService
    {
        private readonly IBallRepository ballRepository;
        private readonly object lockObject = new object();
        Random random = new();
        private CancellationTokenSource cts = new();

        public BallService(IBallRepository repository) => ballRepository = repository;

        public void CreateBalls(int count)
        {
            ballRepository.Clear();
            for (int i = 0; i < count; i++)
            {
                ballRepository.AddBall(
                    random.Next(0, 700 - Ball.Diameter),
                    random.Next(0, 500 - Ball.Diameter)
                );
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
