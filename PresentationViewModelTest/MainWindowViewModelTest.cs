using ConcurrentProgramming.BusinessLogic;
using ConcurrentProgramming.Data;
using ConcurrentProgramming.PresentationModel;
using ConcurrentProgramming.PresentationViewModel;
using Moq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ConcurrentProgramming.PresentationViewModelTests
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        private Mock<BallRepository> mockBallRepository;
        private Mock<BallPhysics> mockBallPhysics;
        private MainWindowViewModel viewModel;

        [TestInitialize]
        public void TestInitialize()
        {
            // Przygotowanie mocków
            mockBallRepository = new Mock<BallRepository>();
            mockBallPhysics = new Mock<BallPhysics>(mockBallRepository.Object);

            // Inicjalizacja ViewModelu
            viewModel = new MainWindowViewModel();
        }

        [TestMethod]
        public void CorrectPropertiesTest()
        {
            // Assert, że komenda RestartSimulationCommand jest zainicjalizowana
            Assert.IsNotNull(viewModel.RestartSimulationCommand);
            Assert.IsInstanceOfType(viewModel.RestartSimulationCommand, typeof(ICommand));

            // Assert, że kolekcja kul jest poprawnie zainicjalizowana
            Assert.IsNotNull(viewModel.Balls);
            Assert.IsInstanceOfType(viewModel.Balls, typeof(ObservableCollection<Ball>));
        }
    }
}
