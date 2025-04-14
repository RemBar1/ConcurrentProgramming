using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.Logic
{
    public interface IBallService
    {
        void AddBall(int ballPositionX, int ballPositionY, int diameter);
        void MoveBall(IBall ball);
        void CreateBalls(int count, int diameter);
        void StartSimulation();
        void StopSimulation();
    }
}
