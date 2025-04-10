using ConcurrentProgramming.Data;

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
    }
}
