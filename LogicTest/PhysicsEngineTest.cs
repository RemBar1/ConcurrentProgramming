using ConcurrentProgramming.Logic.Physics;
using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.LogicTest
{
    [TestClass]
    public class PhysicsEngineTest
    {
        private readonly IPhysicsEngine _physicsEngine = new PhysicsEngine();

        [TestMethod]
        public void HandleLeftWallCollisionTest()
        {
            // Arrange
            var ball = new Ball(1, new Vector2(0, 50), 10) { Velocity = new Vector2(-2, 3) };

            // Act
            _physicsEngine.HandleWallCollision(ball, 100, 100);

            // Assert
            Assert.AreEqual(2, ball.Velocity.X);
            Assert.AreEqual(3, ball.Velocity.Y);
        }

        [TestMethod]
        public void HandleRightWallCollisionTest()
        {
            // Arrange
            var ball = new Ball(1, new Vector2(95, 50), 10) { Velocity = new Vector2(2, 3) };

            // Act
            _physicsEngine.HandleWallCollision(ball, 100, 100);

            // Assert
            Assert.AreEqual(-2, ball.Velocity.X);
            Assert.AreEqual(3, ball.Velocity.Y);
        }

        [TestMethod]
        public void HandleTopWallCollisionTest()
        {
            // Arrange
            var ball = new Ball(1, new Vector2(50, 0), 10) { Velocity = new Vector2(2, -3) };

            // Act
            _physicsEngine.HandleWallCollision(ball, 100, 100);

            // Assert
            Assert.AreEqual(2, ball.Velocity.X);
            Assert.AreEqual(3, ball.Velocity.Y);
        }

        [TestMethod]
        public void HandleBottomWallCollisionTest()
        {
            // Arrange
            var ball = new Ball(1, new Vector2(50, 95), 10) { Velocity = new Vector2(2, 3) };

            // Act
            _physicsEngine.HandleWallCollision(ball, 100, 100);

            // Assert
            Assert.AreEqual(2, ball.Velocity.X);
            Assert.AreEqual(-3, ball.Velocity.Y);
        }

        [TestMethod]
        public void CorrectVelocitiesTest()
        {
            // Arrange
            var ball1 = new Ball(1, new Vector2(0, 0), 10) { Velocity = new Vector2(2, 0) };
            var ball2 = new Ball(2, new Vector2(10, 0), 10) { Velocity = new Vector2(-1, 0) };

            // Act
            _physicsEngine.HandleBallCollision(ball1, ball2);

            // Assert
            Assert.AreNotEqual(new Vector2(2, 0), ball1.Velocity);
            Assert.AreNotEqual(new Vector2(-1, 0), ball2.Velocity);
            // Zachowanie energii i pędu
            var totalMomentumBefore = new Vector2(2 * 100 + -1 * 100, 0);
            var totalMomentumAfter = new Vector2(ball1.Velocity.X * 100 + ball2.Velocity.X * 100, 0);
            Assert.AreEqual(totalMomentumBefore.X, totalMomentumAfter.X, 0.001);
        }
    }
}