using ConcurrentProgramming.Data;
using ConcurrentProgramming.PresentationModel;

namespace ConcurrentProgramming.DataTest
{
    [TestClass]
    public class BallRepositoryTest
    {
        [TestMethod]
        public void RepositoryEmptyTest()
        {
            // Arrange
            var ballRepository = new BallRepository();

            // Act
            var balls = ballRepository.Balls;

            // Assert
            Assert.AreEqual(0, balls.Count, "Kolekcja kulek powinna być początkowo pusta.");
        }

        [TestMethod]
        public void AddBallCollectionCountTest()
        {
            // Arrange
            var ballRepository = new BallRepository();

            // Act
            ballRepository.AddBall(100, 150);

            // Assert
            Assert.AreEqual(1, ballRepository.Balls.Count, "Dodanie kulki powinno zwiększyć liczbę elementów w kolekcji.");
        }

        [TestMethod]
        public void AddBallCorrectPositionTest()
        {
            // Arrange
            var ballRepository = new BallRepository();
            int expectedX = 200, expectedY = 250;

            // Act
            ballRepository.AddBall(expectedX, expectedY);
            Ball addedBall = ballRepository.Balls[0];

            // Assert
            Assert.AreEqual(expectedX, addedBall.PositionX, "Pozycja X kulki powinna być poprawna.");
            Assert.AreEqual(expectedY, addedBall.PositionY, "Pozycja Y kulki powinna być poprawna.");
        }
    }
}
