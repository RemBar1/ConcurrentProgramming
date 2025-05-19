using ConcurrentProgramming.Data;
using ConcurrentProgramming.Logic.Service;
using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.LogicTest
{
    [TestClass]
    public class BallServiceIntegrationTests
    {
        private BallRepository _repository;
        private BallService _ballService;
        private const int TestBoardSize = 500;
        private const int TestBoardThickness = 5;

        [TestInitialize]
        public void Setup()
        {
            _repository = new BallRepository();
            _ballService = new BallService(
                _repository,
                TestBoardSize,
                TestBoardSize,
                TestBoardThickness);
        }

        [TestMethod]
        public void CreateBallsTest()
        {
            // Act
            _ballService.CreateBalls(5, 20);

            // Assert
            Assert.AreEqual(5, _repository.Count);
        }

        [TestMethod]
        public void CreateBallsNoCollisionsTest()
        {
            // Act
            _ballService.CreateBalls(10, 20);

            // Assert
            var balls = _repository.GetAll().ToList();
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    double distance = (balls[i].Position - balls[j].Position).Length;
                    double minDistance = balls[i].Diameter / 2 + balls[j].Diameter / 2;
                    Assert.IsTrue(distance >= minDistance,
                        $"Balls {i} and {j} are too close to each other");
                }
            }
        }

        [TestMethod]
        public void StopSimulationTest()
        {
            // Arrange
            _ballService.CreateBalls(1, 20);
            _ballService.StartSimulation();

            // Act
            _ballService.StopSimulation();
            Thread.Sleep(100); // Czekamy na zatrzymanie

            // Assert
            Assert.IsFalse(_ballService.IsSimulationRunning);
        }

        [TestMethod]
        public void SimulationPositionsTest()
        {
            // Arrange
            _ballService.CreateBalls(1, 20);
            var ball = _repository.GetAll().First();
            var initialPosition = ball.Position;

            // Act
            _ballService.StartSimulation();
            Thread.Sleep(100); // Czekamy na aktualizację

            // Assert
            Assert.AreNotEqual(initialPosition, ball.Position);

            _ballService.StopSimulation();
        }

        [TestMethod]
        public void DisposeTest()
        {
            // Arrange
            _ballService.CreateBalls(1, 20);
            _ballService.StartSimulation();

            // Act
            _ballService.Dispose();

            // Assert
            Assert.IsFalse(_ballService.IsSimulationRunning);
            Assert.AreEqual(0, _repository.Count);
        }
    }
}