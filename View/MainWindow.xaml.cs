using ConcurrentProgramming.Data;
using ConcurrentProgramming.Logic;
using ConcurrentProgramming.ViewModel;
using System.Windows;

namespace ConcurrentProgramming.View
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            InitializeComponent();
            var repository = new BallRepository();
            var physics = new BallPhysics(repository);
            viewModel = new MainWindowViewModel(physics, repository);
            DataContext = viewModel;
        }

        protected override void OnClosed(EventArgs e)
        {
            viewModel?.StopSimulation();
            base.OnClosed(e);
        }
    }
}