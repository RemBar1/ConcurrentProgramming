using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;
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
        List<int> AvailableDiameters { get; }
        void StartSimulation();
        void StopSimulation();

    }
}
