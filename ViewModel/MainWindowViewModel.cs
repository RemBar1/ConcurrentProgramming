using ConcurrentProgramming.Data;
using ConcurrentProgramming.Logic.Service;
using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ConcurrentProgramming.ViewModel
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly IBallService ballService;
        private readonly IBallRepository ballRepository;
        private int ballCount;
        private int selectedDiameter = 20;

        public ObservableCollection<IBall> Balls => ballRepository.Balls;
        public ICommand StartSimulationCommand { get; }
        public ICommand StopSimulationCommand { get; }
        public ICommand ChangeDiameterCommand { get; }
        private bool _isSimulationRunning;
        public int BoardWidth { get; }
        public int BoardHeight { get; }
        public int BoardThickness { get; } = 3;

        public int BallCount
        {
            get => ballCount;
            set => ballCount = Math.Clamp(value, 1, 20);
        }

        public MainWindowViewModel()
        {
            BoardWidth = (int)(SystemParameters.PrimaryScreenWidth * 0.75);
            BoardHeight = (int)(SystemParameters.PrimaryScreenHeight * 0.75);

            ballRepository = new BallRepository();
            ballService = new BallService(ballRepository, BoardWidth, BoardHeight, BoardThickness);
            StartSimulationCommand = new RelayCommand(StartSimulation);
            StopSimulationCommand = new RelayCommand(StopSimulation);
            ChangeDiameterCommand = new RelayCommand(UpdateBallsDiameter);
        }

        public int SelectedDiameter
        {
            get => selectedDiameter;
            set
            {
                if (selectedDiameter != value)
                {
                    selectedDiameter = value;
                    OnPropertyChanged(nameof(SelectedDiameter));
                    UpdateBallsDiameter();
                }
            }
        }

        public List<int> AvailableDiameters { get; } = [10, 20, 30, 40, 50, 60, 70, 80, 90, 100];

        private void UpdateBallsDiameter()
        {
            StartSimulation();
        }

        public void StartSimulation()
        {
            ballService.StopSimulation();
            ballService.CreateBalls(BallCount, SelectedDiameter);
            ballService.StartSimulation();
        }

        public void StopSimulation()
        {
            ballService.StopSimulation();
        }

        public bool IsSimulationRunning
        {
            get => _isSimulationRunning;
            private set
            {
                if (_isSimulationRunning != value)
                {
                    _isSimulationRunning = value;
                    OnPropertyChanged(nameof(IsSimulationRunning));
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
