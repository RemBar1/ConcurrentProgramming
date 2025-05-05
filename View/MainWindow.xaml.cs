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
            _ = BallCountTextBox.Focus();
            viewModel = new MainWindowViewModel();
            DataContext = viewModel;
        }

        protected override void OnClosed(EventArgs e)
        {
            viewModel?.StopSimulation();
            base.OnClosed(e);
        }
    }
}