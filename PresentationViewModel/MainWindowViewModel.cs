using ConcurrentProgramming.BusinessLogic;
using ConcurrentProgramming.Data;
using ConcurrentProgramming.PresentationModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ConcurrentProgramming.PresentationViewModel
{
    public class MainWindowViewModel
    {
        private BallRepository ballRepository;
        private BallPhysics ballPhysics;
        public ObservableCollection<Ball> Balls => ballRepository.Balls;

        public ICommand RestartSimulationCommand { get; }

        public MainWindowViewModel()
        {
            ballRepository = new BallRepository();
            ballPhysics = new BallPhysics(ballRepository);
            RestartSimulationCommand = new RelayCommand(RestartSimulation);
        }

        public void StartSimulation()
        {
            ballPhysics.CreateBalls();
            ballPhysics.StartSimulation();
        }

        public void StopSimulation()
        {
            ballPhysics.StopSimulation();
        }

        private void RestartSimulation()
        {
            StopSimulation();
            ballPhysics.CreateBalls();
            StartSimulation();
        }
    }
}
