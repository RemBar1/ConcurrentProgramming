namespace ConcurrentProgramming.Logic.Service
{
    public interface IBallService
    {
        void CreateBalls(int count, int diameter);
        void StartSimulation();
        void StopSimulation();
    }
}
