using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.Logic
{
    public interface IBallService
    {
        void AddBall(int ballPositionX, int ballPositionY);
        void MoveBall(IBall ball);
        void CreateBalls(int count);
        void StartSimulation();
        void StopSimulation();
    }
}
