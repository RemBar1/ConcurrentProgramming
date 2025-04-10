using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ConcurrentProgramming.ViewModel
{
    public interface IMainWindowViewModel
    {
        ObservableCollection<IBall> Balls { get; }
        ICommand RestartSimulationCommand { get; }
    }
}
