using ConcurrentProgramming.PresentationModel;

namespace ConcurrentProgramming.PresentationModelTest
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void BallConstructorTest()
        {
            // Arrange
            int initialX = 100, initialY = 150;

            // Act
            Ball ball = new Ball(initialX, initialY);

            // Assert
            Assert.AreEqual(initialX, ball.PositionX, "Pozycja X kulki powinna być poprawna po inicjalizacji.");
            Assert.AreEqual(initialY, ball.PositionY, "Pozycja Y kulki powinna być poprawna po inicjalizacji.");
            Assert.AreEqual(20, Ball.Diameter, "Średnica kulki powinna wynosić 20.");
        }

        [TestMethod]
        public void BallMovementTest()
        {
            // Arrange
            Ball ball = new Ball(100, 100);

            // Act
            ball.Move();

            // Assert
            Assert.AreEqual(101, ball.PositionX, "Pozycja X powinna zwiększyć się o 1.");
            Assert.AreEqual(101, ball.PositionY, "Pozycja Y powinna zwiększyć się o 1.");
        }

        [TestMethod]
        public void BallMovementWallBouncingTest()
        {
            // Arrange
            Ball ball = new Ball(680, 480); // Blisko prawej i dolnej krawędzi

            // Act
            ball.Move(); // Odbicie od prawej i dolnej ściany
            ball.Move(); // Powinno iść w przeciwnym kierunku

            // Assert
            Assert.AreEqual(678, ball.PositionX, "Po odbiciu od ściany, pozycja X powinna się zmniejszać.");
            Assert.AreEqual(478, ball.PositionY, "Po odbiciu od ściany, pozycja Y powinna się zmniejszać.");
        }

        [TestMethod]
        public void PropertyChangedTest()
        {
            // Arrange
            Ball ball = new Ball(100, 100);
            bool eventRaised = false;

            ball.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "PositionX" || e.PropertyName == "PositionY")
                {
                    eventRaised = true;
                }
            };

            // Act
            ball.Move();

            // Assert
            Assert.IsTrue(eventRaised, "Zdarzenie PropertyChanged powinno zostać wywołane przy zmianie pozycji.");
        }
    }
}
