    using ConcurrentProgramming.Logic;
    using ConcurrentProgramming.Data;
    using ConcurrentProgramming.Model;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    namespace ConcurrentProgramming.ViewModel
    {
        public class MainWindowViewModel : IMainWindowViewModel
        {
            private readonly IBallPhysics ballPhysics;
            private readonly IBallRepository ballRepository;

            public ObservableCollection<IBall> Balls => ballRepository.Balls;
            public ICommand RestartSimulationCommand { get; }

            public MainWindowViewModel(IBallPhysics ballPhysics, IBallRepository ballRepository)
            {
                this.ballPhysics = ballPhysics;
                this.ballRepository = ballRepository;
                RestartSimulationCommand = new RelayCommand(RestartSimulation);
                StartSimulation();
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

            public void RestartSimulation()
            {
                StopSimulation();
                ballPhysics.CreateBalls();
                StartSimulation();
            }
        }
    }
