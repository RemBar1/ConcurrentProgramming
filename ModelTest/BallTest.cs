using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.ModelTest
{
    [TestClass]
    public class BallTest
    {
        private Ball _ball;
        private bool _propertyChangedFired;

        [TestInitialize]
        public void Setup()
        {
            _ball = new Ball(1, new Vector2(10, 10), 20);
            _ball.PropertyChanged += (sender, args) => _propertyChangedFired = true;
            _propertyChangedFired = false;
        }

        [TestMethod]
        public void ConstructorTest()
        {
            // Assert
            Assert.AreEqual(1, _ball.Id);
            Assert.AreEqual(new Vector2(10, 10), _ball.Position);
            Assert.AreEqual(20, _ball.Diameter);
            Assert.AreEqual(400, _ball.Mass); // 20 * 20
        }

        [TestMethod]
        public void PositionTest()
        {
            // Act
            _ball.Position = new Vector2(20, 20);

            // Assert
            Assert.IsTrue(_propertyChangedFired);
            Assert.AreEqual(new Vector2(20, 20), _ball.Position);
        }

        [TestMethod]
        public void PositionNotChangedTest()
        {
            // Arrange
            var originalPosition = _ball.Position;

            // Act
            _ball.Position = originalPosition;

            // Assert
            Assert.IsFalse(_propertyChangedFired);
        }

        [TestMethod]
        public void DiameterTest()
        {
            // Act
            _ball.Diameter = 30;

            // Assert
            Assert.IsTrue(_propertyChangedFired);
            Assert.AreEqual(30, _ball.Diameter);
        }

        [TestMethod]
        public void VelocityTest()
        {
            // Act
            _ball.Velocity = new Vector2(5, 5);

            // Assert
            Assert.IsTrue(_propertyChangedFired);
            Assert.AreEqual(new Vector2(5, 5), _ball.Velocity);
        }

        [TestMethod]
        public void UpdatePositionTest()
        {
            // Act
            _ball.UpdatePosition(new Vector2(15, 15));

            // Assert
            Assert.AreEqual(new Vector2(15, 15), _ball.Position);
            Assert.IsTrue(_propertyChangedFired);
        }

        [TestMethod]
        public void MassTest()
        {
            // Arrange
            _ball.Diameter = 10;

            // Act & Assert
            Assert.AreEqual(100, _ball.Mass);

            // Arrange
            _ball.Diameter = 25;

            // Act & Assert
            Assert.AreEqual(625, _ball.Mass);
        }

        [TestMethod]
        public void PositionThreadSafeTest()
        {
            // Arrange
            var newPosition = new Vector2(20, 20);
            var positionSet = false;
            var monitor = new object();

            // Act
            var thread = new Thread(() =>
            {
                lock (monitor)
                {
                    _ball.Position = newPosition;
                    positionSet = true;
                    Monitor.Pulse(monitor);
                }
            });

            lock (monitor)
            {
                thread.Start();
                Monitor.Wait(monitor);
            }

            // Assert
            Assert.IsTrue(positionSet);
            Assert.AreEqual(newPosition, _ball.Position);
        }

        [TestMethod]
        public void PropertyChangedTest()
        {
            // Arrange
            var changedProperties = new List<string>();
            _ball.PropertyChanged += (sender, args) => changedProperties.Add(args.PropertyName);

            // Act
            _ball.Position = new Vector2(15, 15);
            _ball.Diameter = 25;
            _ball.Velocity = new Vector2(2, 2);

            // Assert
            Assert.AreEqual(3, changedProperties.Count);
            Assert.IsTrue(changedProperties.Contains(nameof(Ball.Position)));
            Assert.IsTrue(changedProperties.Contains(nameof(Ball.Diameter)));
            Assert.IsTrue(changedProperties.Contains(nameof(Ball.Velocity)));
        }
    }
}