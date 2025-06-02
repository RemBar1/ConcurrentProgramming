using ConcurrentProgramming.ViewModel;
using System.Windows;
using System.Windows.Threading;

namespace ConcurrentProgramming.View
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel viewModel;
        private readonly DispatcherTimer _clockTimer;

        public MainWindow()
        {
            InitializeComponent();

            // Inicjalizacja timera dla zegara
            _clockTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _clockTimer.Tick += UpdateClock;
            _clockTimer.Start();

            UpdateClock(null, null); // Wywołaj od razu aby pokazać czas

            _ = BallCountTextBox.Focus();
            viewModel = new MainWindowViewModel();
            DataContext = viewModel;

        }

        private void UpdateClock(object sender, EventArgs e)
        {
            ClockTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        protected override void OnClosed(EventArgs e)
        {
            viewModel?.StopSimulation();
            if (viewModel is IDisposable disposable)
            {
                disposable.Dispose();
            }
            base.OnClosed(e);
        }
    }
}