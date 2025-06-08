using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConcurrentProgramming.Logic.Physics;
using ConcurrentProgramming.Model;

namespace LogicTest
{
    [TestClass]
    public class PhysicsEngineTest
    {
        private IPhysicsEngine physicsEngine;
        private Ball ball1;
        private Ball ball2;

        [TestInitialize]
        public void Setup()
        {
            physicsEngine = new PhysicsEngine();
            ball1 = new Ball(1, new Vector2(100, 100), 20);
            ball2 = new Ball(2, new Vector2(200, 200), 20);
        }

        [TestMethod]
        public void HandleWallCollision_BallHitsRightWall_ReversesXVelocity()
        {
            // Arrange
            ball1.Position = new Vector2(795, 100);
            ball1.Velocity = new Vector2(10, 0);

            // Act
            bool collision = physicsEngine.HandleWallCollision(ball1, 800, 600);

            // Assert
            Assert.IsTrue(collision);
            Assert.AreEqual(-10, ball1.Velocity.X);
            Assert.AreEqual(0, ball1.Velocity.Y);
        }

        [TestMethod]
        public void HandleWallCollision_BallHitsBottomWall_ReversesYVelocity()
        {
            // Arrange
            ball1.Position = new Vector2(100, 595);
            ball1.Velocity = new Vector2(0, 10);

            // Act
            bool collision = physicsEngine.HandleWallCollision(ball1, 800, 600);

            // Assert
            Assert.IsTrue(collision);
            Assert.AreEqual(0, ball1.Velocity.X);
            Assert.AreEqual(-10, ball1.Velocity.Y);
        }

        [TestMethod]
        public void HandleWallCollision_NoCollision_ReturnsFalse()
        {
            // Arrange
            ball1.Position = new Vector2(100, 100);
            ball1.Velocity = new Vector2(5, 5);

            // Act
            bool collision = physicsEngine.HandleWallCollision(ball1, 800, 600);

            // Assert
            Assert.IsFalse(collision);
            Assert.AreEqual(5, ball1.Velocity.X);
            Assert.AreEqual(5, ball1.Velocity.Y);
        }

        [TestMethod]
        public void HandleBallCollision_BallsCollide_UpdatesVelocities()
        {
            // Arrange
            ball1.Position = new Vector2(100, 100);
            ball2.Position = new Vector2(110, 100);
            ball1.Velocity = new Vector2(10, 0);
            ball2.Velocity = new Vector2(-10, 0);

            // Act
            bool collision = physicsEngine.HandleBallCollision(ball1, ball2);

            // Assert
            Assert.IsTrue(collision);
            Assert.AreEqual(-10, ball1.Velocity.X);
            Assert.AreEqual(10, ball2.Velocity.X);
        }

        [TestMethod]
        public void HandleBallCollision_BallsTooFarApart_ReturnsFalse()
        {
            // Arrange
            ball1.Position = new Vector2(100, 100);
            ball2.Position = new Vector2(200, 200);
            Vector2 originalVelocity1 = new Vector2(5, 0);
            Vector2 originalVelocity2 = new Vector2(-5, 0);
            ball1.Velocity = originalVelocity1;
            ball2.Velocity = originalVelocity2;

            // Act
            bool collision = physicsEngine.HandleBallCollision(ball1, ball2);

            // Assert
            Assert.IsFalse(collision);
            Assert.AreEqual(originalVelocity1, ball1.Velocity);
            Assert.AreEqual(originalVelocity2, ball2.Velocity);
        }
    }
} 