using ConcurrentProgramming.Data;
using ConcurrentProgramming.Logic;
using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ConcurrentProgramming.ViewModel
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly IBallService ballPhysics;
        private readonly IBallRepository ballRepository;
        private int ballCount = 5;

        public ObservableCollection<IBall> Balls => ballRepository.Balls;
        public ICommand StartSimulationCommand { get; }
        public ICommand StopSimulationCommand { get; }

        public int BallCount
        {
            get => ballCount;
            set
            {
                if (value > 0 && value <= 10)  // Ograniczenie do 10 kul
                {
                    ballCount = value;
                }
            }
        }
        public MainWindowViewModel(IBallService ballPhysics, IBallRepository ballRepository)
        {
            this.ballPhysics = ballPhysics;
            this.ballRepository = ballRepository;
            StartSimulationCommand = new RelayCommand(StartSimulation);
            StopSimulationCommand = new RelayCommand(StopSimulation);
        }

        public void StartSimulation()
        {
            ballPhysics.CreateBalls(BallCount);
            ballPhysics.StartSimulation();
        }

        public void StopSimulation() => ballPhysics.StopSimulation();
    }
}
