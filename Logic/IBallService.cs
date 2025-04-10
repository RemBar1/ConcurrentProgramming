namespace ConcurrentProgramming.Logic
{
    public interface IBallService
    {
        void StartSimulation();
        void StopSimulation();
        void CreateBalls(int count);
    }
}
