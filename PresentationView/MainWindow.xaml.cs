using ConcurrentProgramming.PresentationViewModel;
using System.Windows;

namespace ConcurrentProgramming.PresentationView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = (MainWindowViewModel)DataContext;
            viewModel.StartSimulation();
        }

        protected override void OnClosed(EventArgs e)
        {
            viewModel?.StopSimulation();
            base.OnClosed(e);
        }
    }
}