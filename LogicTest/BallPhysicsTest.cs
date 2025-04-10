using ConcurrentProgramming.Data;
using ConcurrentProgramming.Logic;
using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ConcurrentProgramming.LogicTest
{
    [TestClass]
    public class BallPhysicsTest
    {
        private TestBallRepository _testRepository;
        private BallPhysics _physics;

        [TestInitialize]
        public void Setup()
        {
            _testRepository = new TestBallRepository();
            _physics = new BallPhysics(_testRepository);
        }

        // Testowa implementacja IBallRepository
        private class TestBallRepository : IBallRepository
        {
            public ObservableCollection<IBall> Balls { get; } = new();
            public int AddBallCallCount { get; private set; }
            public int ClearCallCount { get; private set; }

            public void AddBall(int x, int y)
            {
                Balls.Add(new TestBall(x, y));
                AddBallCallCount++;
            }

            public void Clear()
            {
                Balls.Clear();
                ClearCallCount++;
            }
        }

        // Testowa implementacja IBall
        private class TestBall : IBall
        {
            public int PositionX { get; set; }
            public int PositionY { get; set; }
            public int MoveCallCount { get; private set; }
            public event PropertyChangedEventHandler? PropertyChanged;

            public TestBall(int x, int y)
            {
                PositionX = x;
                PositionY = y;
            }

            public void Move() => MoveCallCount++;
        }

        [TestMethod]
        public void CreateBalls_ShouldClearAndAddRandomBalls()
        {
            // Act
            _physics.CreateBalls();

            // Assert
            Assert.AreEqual(1, _testRepository.ClearCallCount);
            Assert.IsTrue(_testRepository.Balls.Count >= 5 && _testRepository.Balls.Count <= 10);
        }

        [TestMethod]
        public void StartSimulation_ShouldMoveAllBalls()
        {
            // Arrange
            _testRepository.AddBall(100, 100);
            _testRepository.AddBall(200, 200);
            var ball1 = (TestBall)_testRepository.Balls[0];
            var ball2 = (TestBall)_testRepository.Balls[1];

            // Act
            _physics.StartSimulation();
            Thread.Sleep(100); // Czekamy na wykonanie ruchów
            _physics.StopSimulation();

            // Assert
            Assert.IsTrue(ball1.MoveCallCount > 0);
            Assert.IsTrue(ball2.MoveCallCount > 0);
        }

        [TestMethod]
        public void StopSimulation_ShouldStopMovement()
        {
            // Arrange
            _testRepository.AddBall(100, 100);
            var ball = (TestBall)_testRepository.Balls[0];
            _physics.StartSimulation();

            // Act
            Thread.Sleep(50);
            var callsBeforeStop = ball.MoveCallCount;
            _physics.StopSimulation();
            Thread.Sleep(50);
            var callsAfterStop = ball.MoveCallCount;

            // Assert
            Assert.IsTrue(callsBeforeStop > 0);
            Assert.AreEqual(callsBeforeStop, callsAfterStop);
        }

        [TestMethod]
        public void CreateBalls_ShouldGenerateBallsWithinBounds()
        {
            // Act
            _physics.CreateBalls();

            // Assert
            foreach (var ball in _testRepository.Balls)
            {
                Assert.IsTrue(ball.PositionX >= 0 && ball.PositionX <= 600);
                Assert.IsTrue(ball.PositionY >= 0 && ball.PositionY <= 400);
            }
        }

        [TestMethod]
        public void Simulation_ShouldNotMoveBallsWhenNoneExist()
        {
            // Act
            _physics.StartSimulation();
            Thread.Sleep(100);
            _physics.StopSimulation();

            // Assert
            Assert.AreEqual(0, _testRepository.Balls.Count);
        }
    }
}