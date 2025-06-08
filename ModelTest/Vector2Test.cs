using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConcurrentProgramming.Model;

namespace ModelTest
{
    [TestClass]
    public class Vector2Test
    {
        [TestMethod]
        public void Constructor_ValidValues_CreatesVector()
        {
            // Arrange & Act
            Vector2 vector = new Vector2(3.0, 4.0);

            // Assert
            Assert.AreEqual(3.0, vector.X);
            Assert.AreEqual(4.0, vector.Y);
        }

        [TestMethod]
        public void Length_Vector3And4_Returns5()
        {
            // Arrange
            Vector2 vector = new Vector2(3.0, 4.0);

            // Act
            double length = vector.Length;

            // Assert
            Assert.AreEqual(5.0, length);
        }

        [TestMethod]
        public void Addition_TwoVectors_ReturnsCorrectSum()
        {
            // Arrange
            Vector2 v1 = new Vector2(1.0, 2.0);
            Vector2 v2 = new Vector2(3.0, 4.0);

            // Act
            Vector2 result = v1 + v2;

            // Assert
            Assert.AreEqual(4.0, result.X);
            Assert.AreEqual(6.0, result.Y);
        }

        [TestMethod]
        public void Subtraction_TwoVectors_ReturnsCorrectDifference()
        {
            // Arrange
            Vector2 v1 = new Vector2(3.0, 4.0);
            Vector2 v2 = new Vector2(1.0, 2.0);

            // Act
            Vector2 result = v1 - v2;

            // Assert
            Assert.AreEqual(2.0, result.X);
            Assert.AreEqual(2.0, result.Y);
        }

        [TestMethod]
        public void Multiplication_VectorByScalar_ReturnsCorrectProduct()
        {
            // Arrange
            Vector2 vector = new Vector2(2.0, 3.0);
            double scalar = 2.0;

            // Act
            Vector2 result = vector * scalar;

            // Assert
            Assert.AreEqual(4.0, result.X);
            Assert.AreEqual(6.0, result.Y);
        }

        [TestMethod]
        public void Normalized_NonZeroVector_ReturnsNormalizedVector()
        {
            // Arrange
            Vector2 vector = new Vector2(3.0, 4.0);

            // Act
            Vector2 normalized = vector.Normalized();

            // Assert
            Assert.AreEqual(0.6, normalized.X, 0.000001);
            Assert.AreEqual(0.8, normalized.Y, 0.000001);
        }

        [TestMethod]
        public void Dot_TwoVectors_ReturnsCorrectProduct()
        {
            // Arrange
            Vector2 v1 = new Vector2(1.0, 2.0);
            Vector2 v2 = new Vector2(3.0, 4.0);

            // Act
            double result = v1.Dot(v2);

            // Assert
            Assert.AreEqual(11.0, result);
        }

        [TestMethod]
        public void Equals_SameValues_ReturnsTrue()
        {
            // Arrange
            Vector2 v1 = new Vector2(1.0, 2.0);
            Vector2 v2 = new Vector2(1.0, 2.0);

            // Act & Assert
            Assert.IsTrue(v1.Equals(v2));
            Assert.IsTrue(v1 == v2);
        }

        [TestMethod]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            // Arrange
            Vector2 v1 = new Vector2(1.0, 2.0);
            Vector2 v2 = new Vector2(2.0, 1.0);

            // Act & Assert
            Assert.IsFalse(v1.Equals(v2));
            Assert.IsTrue(v1 != v2);
        }
    }
} 