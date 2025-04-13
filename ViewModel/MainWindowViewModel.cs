using ConcurrentProgramming.Data;
using ConcurrentProgramming.Logic;
using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace ConcurrentProgramming.ViewModel
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly IBallService ballService;
        private readonly IBallRepository ballRepository;
        private int ballCount;

        public ObservableCollection<IBall> Balls => ballRepository.Balls;
        public ICommand StartSimulationCommand { get; }
        public ICommand StopSimulationCommand { get; }
        public int BoardWidth { get; private set; }
        public int BoardHeight { get; private set; }
        public int BoardThickness { get; } = 3;

        public int BallCount
        {
            get => ballCount;
            set => ballCount = Math.Clamp(value, 1, 50);
        }

        public MainWindowViewModel()
        {
            BoardWidth = (int)(SystemParameters.PrimaryScreenWidth * 0.75);
            BoardHeight = (int)(SystemParameters.PrimaryScreenHeight * 0.75);

            ballRepository = new BallRepository();
            ballService = new BallService(ballRepository, BoardWidth, BoardHeight, BoardThickness);
            StartSimulationCommand = new RelayCommand(StartSimulation);
            StopSimulationCommand = new RelayCommand(StopSimulation);
        }

        public void StartSimulation()
        {
            ballService.StopSimulation();
            ballService.CreateBalls(BallCount);
            ballService.StartSimulation();
        }

        public void StopSimulation() => ballService.StopSimulation();
    }
}
