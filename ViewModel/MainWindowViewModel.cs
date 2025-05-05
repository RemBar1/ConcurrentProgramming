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
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public int BoardThickness { get; set; } = 3;

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

        public List<int> AvailableDiameters { get; } = [10, 20, 30, 50];

        private void UpdateBallsDiameter()
        {
            //foreach (IBall ball in Balls)
            //{
            //    ball.Diameter = SelectedDiameter;
            //}
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

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
