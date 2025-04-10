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
            BallCountTextBox.Focus();
            var repository = new BallRepository();
            var physics = new BallService(repository);
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