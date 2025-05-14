using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.ModelTest
{
    [TestClass]
    public class Vector2Test
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // Act
            var vector = new Vector2(1.5, 2.5);

            // Assert
            Assert.AreEqual(1.5, vector.X);
            Assert.AreEqual(2.5, vector.Y);
        }

        [TestMethod]
        public void LengthTest()
        {
            // Arrange
            var vector = new Vector2(3, 4);

            // Act & Assert
            Assert.AreEqual(5, vector.Length);
        }

        [TestMethod]
        public void AdditionOperatorTest()
        {
            // Arrange
            var a = new Vector2(1, 2);
            var b = new Vector2(3, 4);

            // Act
            var result = a + b;

            // Assert
            Assert.AreEqual(4, result.X);
            Assert.AreEqual(6, result.Y);
        }

        [TestMethod]
        public void SubtractionOperatorTest()
        {
            // Arrange
            var a = new Vector2(5, 6);
            var b = new Vector2(3, 4);

            // Act
            var result = a - b;

            // Assert
            Assert.AreEqual(2, result.X);
            Assert.AreEqual(2, result.Y);
        }

        [TestMethod]
        public void MultiplicationOperatorTest()
        {
            // Arrange
            var vector = new Vector2(2, 3);
            double scalar = 1.5;

            // Act
            var result = vector * scalar;

            // Assert
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(4.5, result.Y);
        }

        [TestMethod]
        public void NormalizedTest()
        {
            // Arrange
            var vector = new Vector2(3, 4);

            // Act
            var normalized = vector.Normalized();

            // Assert
            Assert.AreEqual(0.6, normalized.X, 0.0001);
            Assert.AreEqual(0.8, normalized.Y, 0.0001);
            Assert.AreEqual(1, normalized.Length, 0.0001);
        }

        [TestMethod]
        public void DotTest()
        {
            // Arrange
            var a = new Vector2(1, 2);
            var b = new Vector2(3, 4);

            // Act
            var dotProduct = a.Dot(b);

            // Assert
            Assert.AreEqual(11, dotProduct);
        }

        [TestMethod]
        public void EqualsTest()
        {
            // Arrange
            var a = new Vector2(1, 2);
            var b = new Vector2(1, 2);

            // Act & Assert
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a == b);
        }

        [TestMethod]
        public void NotEqualsTest()
        {
            // Arrange
            var a = new Vector2(1, 2);
            var b = new Vector2(3, 4);

            // Act & Assert
            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a != b);
        }
    }
}