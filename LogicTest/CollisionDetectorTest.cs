using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConcurrentProgramming.Logic.Collision;
using ConcurrentProgramming.Model;
using System.Collections.Generic;
using System.Linq;

namespace LogicTest
{
    [TestClass]
    public class CollisionDetectorTest
    {
        private ICollisionDetector collisionDetector;
        private List<IBall> balls;

        [TestInitialize]
        public void Setup()
        {
            collisionDetector = new CollisionDetector();
            balls = new List<IBall>();
        }

        [TestMethod]
        public void DetectCollisions_NoCollisions_ReturnsEmptyList()
        {
            // Arrange
            balls.Add(new Ball(1, new Vector2(100, 100), 20));
            balls.Add(new Ball(2, new Vector2(200, 200), 20));
            balls.Add(new Ball(3, new Vector2(300, 300), 20));

            // Act
            var collisions = collisionDetector.DetectCollisions(balls);

            // Assert
            Assert.IsFalse(collisions.Any());
        }

        [TestMethod]
        public void DetectCollisions_TwoBallsColliding_ReturnsOnePair()
        {
            // Arrange
            Ball ball1 = new Ball(1, new Vector2(100, 100), 20);
            Ball ball2 = new Ball(2, new Vector2(110, 100), 20);
            balls.Add(ball1);
            balls.Add(ball2);

            // Act
            var collisions = collisionDetector.DetectCollisions(balls).ToList();

            // Assert
            Assert.AreEqual(1, collisions.Count);
            var collision = collisions[0];
            Assert.IsTrue((collision.Item1 == ball1 && collision.Item2 == ball2) ||
                         (collision.Item1 == ball2 && collision.Item2 == ball1));
        }

        [TestMethod]
        public void DetectCollisions_MultipleBallsColliding_ReturnsAllPairs()
        {
            // Arrange
            Ball ball1 = new Ball(1, new Vector2(100, 100), 20);
            Ball ball2 = new Ball(2, new Vector2(110, 100), 20);
            Ball ball3 = new Ball(3, new Vector2(100, 110), 20);
            balls.Add(ball1);
            balls.Add(ball2);
            balls.Add(ball3);

            // Act
            var collisions = collisionDetector.DetectCollisions(balls).ToList();

            // Assert
            Assert.AreEqual(3, collisions.Count); // All three balls are close enough to collide with each other
        }

        [TestMethod]
        public void DetectCollisions_EmptyList_ReturnsEmptyList()
        {
            // Act
            var collisions = collisionDetector.DetectCollisions(balls);

            // Assert
            Assert.IsFalse(collisions.Any());
        }

        [TestMethod]
        public void DetectCollisions_SingleBall_ReturnsEmptyList()
        {
            // Arrange
            balls.Add(new Ball(1, new Vector2(100, 100), 20));

            // Act
            var collisions = collisionDetector.DetectCollisions(balls);

            // Assert
            Assert.IsFalse(collisions.Any());
        }

        [TestMethod]
        public void DetectCollisions_BallsAtSamePosition_ReturnsCollision()
        {
            // Arrange
            Ball ball1 = new Ball(1, new Vector2(100, 100), 20);
            Ball ball2 = new Ball(2, new Vector2(100, 100), 20);
            balls.Add(ball1);
            balls.Add(ball2);

            // Act
            var collisions = collisionDetector.DetectCollisions(balls).ToList();

            // Assert
            Assert.AreEqual(1, collisions.Count);
            var collision = collisions[0];
            Assert.IsTrue((collision.Item1 == ball1 && collision.Item2 == ball2) ||
                         (collision.Item1 == ball2 && collision.Item2 == ball1));
        }
    }
} 