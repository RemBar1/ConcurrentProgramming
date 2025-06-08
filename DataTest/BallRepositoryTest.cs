using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConcurrentProgramming.Data;
using ConcurrentProgramming.Model;
using System.Linq;

namespace DataTest
{
    [TestClass]
    public class BallRepositoryTest
    {
        private IBallRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = new BallRepository();
        }

        [TestMethod]
        public void Add_NewBall_AddsSuccessfully()
        {
            // Arrange
            Ball ball = new Ball(1, new Vector2(100, 100), 20);

            // Act
            repository.Add(ball);

            // Assert
            Assert.AreEqual(1, repository.GetAll().Count());
            Assert.AreEqual(ball, repository.GetAll().First());
        }

        [TestMethod]
        public void Add_MultipleBalls_AddsAllSuccessfully()
        {
            // Arrange
            Ball ball1 = new Ball(1, new Vector2(100, 100), 20);
            Ball ball2 = new Ball(2, new Vector2(200, 200), 20);
            Ball ball3 = new Ball(3, new Vector2(300, 300), 20);

            // Act
            repository.Add(ball1);
            repository.Add(ball2);
            repository.Add(ball3);

            // Assert
            Assert.AreEqual(3, repository.GetAll().Count());
            CollectionAssert.Contains(repository.GetAll().ToList(), ball1);
            CollectionAssert.Contains(repository.GetAll().ToList(), ball2);
            CollectionAssert.Contains(repository.GetAll().ToList(), ball3);
        }

        [TestMethod]
        public void GetAll_EmptyRepository_ReturnsEmptyCollection()
        {
            // Act
            var balls = repository.GetAll();

            // Assert
            Assert.IsFalse(balls.Any());
        }

        [TestMethod]
        public void Clear_WithBalls_RemovesAllBalls()
        {
            // Arrange
            repository.Add(new Ball(1, new Vector2(100, 100), 20));
            repository.Add(new Ball(2, new Vector2(200, 200), 20));

            // Act
            repository.Clear();

            // Assert
            Assert.IsFalse(repository.GetAll().Any());
        }

        [TestMethod]
        public void Clear_EmptyRepository_DoesNotThrowException()
        {
            // Act & Assert
            repository.Clear(); // Should not throw
            Assert.IsFalse(repository.GetAll().Any());
        }

        [TestMethod]
        public void GetAll_ReturnsCopy_ModifyingReturnDoesNotAffectRepository()
        {
            // Arrange
            Ball ball1 = new Ball(1, new Vector2(100, 100), 20);
            Ball ball2 = new Ball(2, new Vector2(200, 200), 20);
            repository.Add(ball1);
            repository.Add(ball2);

            // Act
            var balls = repository.GetAll().ToList();
            balls.Clear(); // Modify the returned collection

            // Assert
            Assert.AreEqual(2, repository.GetAll().Count()); // Original repository should be unchanged
        }

        [TestMethod]
        public void ThreadSafety_ConcurrentGetAll_ReturnsValidCollection()
        {
            // Arrange
            const int iterations = 100;
            const int initialBallCount = 10;
            for (int i = 0; i < initialBallCount; i++)
            {
                repository.Add(new Ball(i, new Vector2(100, 100), 20));
            }

            // Act & Assert
            Parallel.For(0, iterations, _ =>
            {
                var balls = repository.GetAll();
                Assert.IsNotNull(balls);
                Assert.IsTrue(balls.Count() <= initialBallCount);
            });
        }
    }
} 