using ConcurrentProgramming.Data;
using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ConcurrentProgramming.DataTest
{
    [TestClass]
    public class BallRepositoryTest
    {
        private BallRepository _repository;
        private TestBall _testBall;

        [TestInitialize]
        public void Setup()
        {
            _repository = new BallRepository();
            _testBall = new TestBall(10, 20);
        }

        // Test pomocniczy - mock Ball bez Moq
        private class TestBall : IBall
        {
            public int PositionX { get; set; }
            public int PositionY { get; set; }
            public event PropertyChangedEventHandler? PropertyChanged;

            public TestBall(int x, int y)
            {
                PositionX = x;
                PositionY = y;
            }

            public void Move() { /* Nie potrzebujemy implementacji do testów */ }
        }

        [TestMethod]
        public void Constructor_InitializesEmptyCollection()
        {
            Assert.AreEqual(0, _repository.Balls.Count);
        }

        [TestMethod]
        public void AddBall_AddsNewBallWithCorrectPositions()
        {
            // Act
            _repository.AddBall(100, 150);

            // Assert
            Assert.AreEqual(1, _repository.Balls.Count);
            var ball = _repository.Balls[0];
            Assert.AreEqual(100, ball.PositionX);
            Assert.AreEqual(150, ball.PositionY);
        }

        [TestMethod]
        public void Clear_RemovesAllBalls()
        {
            // Arrange
            _repository.AddBall(10, 10);
            _repository.AddBall(20, 20);

            // Act
            _repository.Clear();

            // Assert
            Assert.AreEqual(0, _repository.Balls.Count);
        }

        [TestMethod]
        public void BallsProperty_CanReplaceEntireCollection()
        {
            // Arrange
            var newCollection = new ObservableCollection<IBall>
            {
                _testBall,
                new TestBall(30, 40)
            };

            // Act
            _repository.Balls = newCollection;

            // Assert
            Assert.AreEqual(2, _repository.Balls.Count);
            Assert.AreSame(newCollection, _repository.Balls);
        }

        [TestMethod]
        public void AddBall_MultipleTimes_IncreasesCountCorrectly()
        {
            // Act
            _repository.AddBall(1, 1);
            _repository.AddBall(2, 2);
            _repository.AddBall(3, 3);

            // Assert
            Assert.AreEqual(3, _repository.Balls.Count);
        }

        [TestMethod]
        public void BallsProperty_ReflectsChanges()
        {
            // Arrange
            var initialCount = _repository.Balls.Count;

            // Act
            _repository.AddBall(5, 5);

            // Assert
            Assert.AreEqual(initialCount + 1, _repository.Balls.Count);
        }
    }
}