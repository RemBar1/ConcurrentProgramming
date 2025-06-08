using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConcurrentProgramming.Model;
using System.Drawing;
using System.ComponentModel;

namespace ModelTest
{
    [TestClass]
    public class BallTest
    {
        private Ball ball;
        private bool propertyChanged;
        private string lastPropertyName;

        [TestInitialize]
        public void Setup()
        {
            ball = new Ball(1, new Vector2(100, 100), 20);
            propertyChanged = false;
            lastPropertyName = string.Empty;
            ball.PropertyChanged += Ball_PropertyChanged;
        }

        private void Ball_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            propertyChanged = true;
            lastPropertyName = e.PropertyName;
        }

        [TestMethod]
        public void Constructor_ValidParameters_CreatesCorrectBall()
        {
            // Assert
            Assert.AreEqual(1, ball.Id);
            Assert.AreEqual(100, ball.Position.X);
            Assert.AreEqual(100, ball.Position.Y);
            Assert.AreEqual(20, ball.Diameter);
            Assert.AreEqual(400, ball.Mass); // Mass = diameter * diameter
        }

        [TestMethod]
        public void Position_SetNewValue_RaisesPropertyChanged()
        {
            // Arrange
            Vector2 newPosition = new Vector2(200, 200);

            // Act
            ball.Position = newPosition;

            // Assert
            Assert.IsTrue(propertyChanged);
            Assert.AreEqual(nameof(Ball.Position), lastPropertyName);
            Assert.AreEqual(newPosition, ball.Position);
        }

        [TestMethod]
        public void Position_SetSameValue_DoesNotRaisePropertyChanged()
        {
            // Arrange
            Vector2 samePosition = new Vector2(100, 100);

            // Act
            ball.Position = samePosition;

            // Assert
            Assert.IsFalse(propertyChanged);
        }

        [TestMethod]
        public void Diameter_SetNewValue_RaisesPropertyChanged()
        {
            // Act
            ball.Diameter = 30;

            // Assert
            Assert.IsTrue(propertyChanged);
            Assert.AreEqual(nameof(Ball.Diameter), lastPropertyName);
            Assert.AreEqual(30, ball.Diameter);
            Assert.AreEqual(900, ball.Mass); // New mass should be 30 * 30
        }

        [TestMethod]
        public void Diameter_SetSameValue_DoesNotRaisePropertyChanged()
        {
            // Act
            ball.Diameter = 20;

            // Assert
            Assert.IsFalse(propertyChanged);
        }

        [TestMethod]
        public void Velocity_SetNewValue_RaisesPropertyChanged()
        {
            // Arrange
            Vector2 newVelocity = new Vector2(5, 5);

            // Act
            ball.Velocity = newVelocity;

            // Assert
            Assert.IsTrue(propertyChanged);
            Assert.AreEqual(nameof(Ball.Velocity), lastPropertyName);
            Assert.AreEqual(newVelocity, ball.Velocity);
        }

        [TestMethod]
        public void Velocity_SetSameValue_DoesNotRaisePropertyChanged()
        {
            // Arrange
            Vector2 velocity = new Vector2(0, 0);
            ball.Velocity = velocity;
            propertyChanged = false;

            // Act
            ball.Velocity = velocity;

            // Assert
            Assert.IsFalse(propertyChanged);
        }

        [TestMethod]
        public void Color_SetNewValue_RaisesPropertyChanged()
        {
            // Act
            ball.Color = Color.Red;

            // Assert
            Assert.IsTrue(propertyChanged);
            Assert.AreEqual(nameof(Ball.Color), lastPropertyName);
            Assert.AreEqual(Color.Red, ball.Color);
        }

        [TestMethod]
        public void Color_SetSameValue_DoesNotRaisePropertyChanged()
        {
            // Arrange
            ball.Color = Color.Blue;
            propertyChanged = false;

            // Act
            ball.Color = Color.Blue;

            // Assert
            Assert.IsFalse(propertyChanged);
        }

        [TestMethod]
        public void UpdatePosition_NewPosition_UpdatesPositionAndRaisesPropertyChanged()
        {
            // Arrange
            Vector2 newPosition = new Vector2(300, 300);

            // Act
            ball.UpdatePosition(newPosition);

            // Assert
            Assert.IsTrue(propertyChanged);
            Assert.AreEqual(nameof(Ball.Position), lastPropertyName);
            Assert.AreEqual(newPosition, ball.Position);
        }

        [TestMethod]
        public void ThreadSafety_ConcurrentPositionUpdates_NoDataRace()
        {
            // Arrange
            const int iterations = 1000;
            Vector2 finalPosition = new Vector2(200, 200);

            // Act
            Parallel.For(0, iterations, _ =>
            {
                ball.UpdatePosition(finalPosition);
            });

            // Assert
            Assert.AreEqual(finalPosition, ball.Position);
        }
    }
} 