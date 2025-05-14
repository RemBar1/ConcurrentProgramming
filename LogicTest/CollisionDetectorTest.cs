using ConcurrentProgramming.Logic.Collision;
using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.LogicTest
{
    [TestClass]
    public class CollisionDetectorTest
    {
        private readonly ICollisionDetector _collisionDetector = new CollisionDetector();

        [TestMethod]
        public void DetectNoCollisionsTest()
        {
            // Arrange
            var balls = new[]
            {
                new Ball(1, new Vector2(0, 0), 10),
                new Ball(2, new Vector2(20, 20), 10)
            };

            // Act
            var collisions = _collisionDetector.DetectCollisions(balls);

            // Assert
            Assert.AreEqual(0, collisions.Count());
        }

        [TestMethod]
        public void DetectCollidingBallsTest()
        {
            // Arrange
            var ball1 = new Ball(1, new Vector2(0, 0), 10);
            var ball2 = new Ball(2, new Vector2(9, 0), 10);
            var balls = new[] { ball1, ball2 };

            // Act
            var collisions = _collisionDetector.DetectCollisions(balls).ToList();

            // Assert
            Assert.AreEqual(1, collisions.Count);
            Assert.IsTrue(collisions.Contains((ball1, ball2)) || collisions.Contains((ball2, ball1)));
        }
    }
}