using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ConcurrentProgramming.ViewModel
{
    public interface IMainWindowViewModel
    {
        ObservableCollection<IBall> Balls { get; }
        ICommand StartSimulationCommand { get; }
        ICommand StopSimulationCommand { get; }
        ICommand ChangeDiameterCommand { get; }
        int BoardWidth { get; set; }
        int BoardHeight { get; set; }
        int BoardThickness { get; set; }
        int BallCount { get; set; }
        int SelectedDiameter { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
        List<int> AvailableDiameters { get; }
        void UpdateBallsDiameter();
        void StartSimulation();
        void StopSimulation();

    }
}
