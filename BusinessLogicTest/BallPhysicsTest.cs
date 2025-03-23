using ConcurrentProgramming.BusinessLogic;
using ConcurrentProgramming.Data;

namespace ConcurrentProgramming.BusinessLogicTest
{
    [TestClass]
    public class BallPhysicsTests
    {
        [TestMethod]
        public void CreateBallsTest()
        {
            // Arrange
            BallRepository repository = new BallRepository();
            BallPhysics physics = new BallPhysics(repository);

            // Act
            physics.CreateBalls();

            // Assert
            Assert.IsTrue(repository.Balls.Count >= 5 && repository.Balls.Count <= 10,
                "Liczba kulek powinna być w zakresie 5-10");
        }

        [TestMethod]
        public void StopSimulationTest()
        {
            // Arrange
            BallRepository repository = new BallRepository();
            BallPhysics physics = new BallPhysics(repository);
            physics.CreateBalls();
            physics.StartSimulation();

            // Act
            Thread.Sleep(50); // Dajemy chwilę na rozpoczęcie symulacji
            physics.StopSimulation();
            var positionsBefore = repository.Balls.Select(b => (b.PositionX, b.PositionY)).ToList();

            Thread.Sleep(50); // Czekamy i sprawdzamy, czy kulki nadal się poruszają
            var positionsAfter = repository.Balls.Select(b => (b.PositionX, b.PositionY)).ToList();

            // Assert
            CollectionAssert.AreEqual(positionsBefore, positionsAfter,
                "Po zatrzymaniu symulacji pozycje kulek nie powinny się zmieniać.");
        }
    }
}
