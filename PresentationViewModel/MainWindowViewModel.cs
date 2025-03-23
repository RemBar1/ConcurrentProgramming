using ConcurrentProgramming.BusinessLogic;
using ConcurrentProgramming.Data;
using ConcurrentProgramming.PresentationModel;
using System.Collections.ObjectModel;

namespace ConcurrentProgramming.PresentationViewModel
{
    public class MainWindowViewModel
    {
        private BallRepository ballRepository;
        private BallPhysics ballPhysics;
        public ObservableCollection<Ball> Balls => ballRepository.Balls;

        public MainWindowViewModel()
        {
            ballRepository = new BallRepository();
            ballPhysics = new BallPhysics(ballRepository);

            ballPhysics.CreateBalls();
        }

        public void StartSimulation()
        {
            ballPhysics.StartSimulation();
        }

        public void StopSimulation()
        {
            ballPhysics.StopSimulation();
        }
    }
}
