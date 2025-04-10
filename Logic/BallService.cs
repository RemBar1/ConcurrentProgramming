using ConcurrentProgramming.Data;
using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.Logic
{
    public class BallService : IBallService
    {
        private readonly IBallRepository ballRepository;
        private readonly object lockObject = new object();
        private CancellationTokenSource cts = new();

        public BallService(IBallRepository repository) => ballRepository = repository;

        public void CreateBalls(int count)
        {
            ballRepository.Clear();
            var random = new Random();
            const int maxAttempts = 100; // Maksymalna liczba prób losowania pozycji na kulkę

            for (int i = 0; i < count; i++)
            {
                int attempts = 0;
                bool positionValid;
                int x, y;

                do
                {
                    x = random.Next(0, Ball.MaxWidth - Ball.Diameter);
                    y = random.Next(0, Ball.MaxHeight - Ball.Diameter);
                    positionValid = true;

                    // Sprawdzenie kolizji z istniejącymi kulkami
                    foreach (var existingBall in ballRepository.Balls)
                    {
                        if (Math.Abs(existingBall.PositionX - x) < Ball.Diameter &&
                            Math.Abs(existingBall.PositionY - y) < Ball.Diameter)
                        {
                            positionValid = false;
                            break;
                        }
                    }

                    attempts++;
                    if (attempts >= maxAttempts)
                    {
                        throw new InvalidOperationException("Nie udało się wygenerować kuli bez kolizji.");
                    }

                } while (!positionValid);

                ballRepository.AddBall(x, y);
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
