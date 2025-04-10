//using ConcurrentProgramming.Model;

//namespace ConcurrentProgramming.ModelTest
//{
//    [TestClass]
//    public class BallTests
//    {
//        private Ball _ball;
//        private bool _propertyChangedFired;

//        [TestInitialize]
//        public void Setup()
//        {
//            _ball = new Ball(100, 100);
//            _propertyChangedFired = false;
//            _ball.PropertyChanged += (sender, e) => _propertyChangedFired = true;
//        }

//        [TestMethod]
//        public void Constructor_SetsInitialPositionAndVelocity()
//        {
//            Assert.AreEqual(100, _ball.PositionX);
//            Assert.AreEqual(100, _ball.PositionY);
//            Assert.AreEqual(1, _ball.VelocityX);
//            Assert.AreEqual(1, _ball.VelocityY);
//        }

//        [TestMethod]
//        public void Move_ChangesPosition()
//        {
//            // Act
//            _ball.Move();

//            // Assert
//            Assert.AreEqual(101, _ball.PositionX);
//            Assert.AreEqual(101, _ball.PositionY);
//            Assert.IsTrue(_propertyChangedFired);
//        }

//        [TestMethod]
//        public void Move_BouncesFromRightWall()
//        {
//            // Arrange
//            var ball = new Ball(680, 100) { VelocityX = 2 }; // 680 + 20 + 2 > 700

//            // Act
//            ball.Move();

//            // Assert
//            Assert.AreEqual(678, ball.PositionX); // 680 + (-2)
//            Assert.AreEqual(-2, ball.VelocityX);
//        }

//        [TestMethod]
//        public void Move_BouncesFromLeftWall()
//        {
//            // Arrange
//            var ball = new Ball(5, 100) { VelocityX = -10 }; // 5 - 10 + 20 < 0

//            // Act
//            ball.Move();

//            // Assert
//            Assert.AreEqual(15, ball.PositionX); // 5 + 10
//            Assert.AreEqual(10, ball.VelocityX);
//        }

//        [TestMethod]
//        public void Move_BouncesFromTopWall()
//        {
//            // Arrange
//            var ball = new Ball(100, 5) { VelocityY = -10 }; // 5 - 10 + 20 < 0

//            // Act
//            ball.Move();

//            // Assert
//            Assert.AreEqual(15, ball.PositionY);
//            Assert.AreEqual(10, ball.VelocityY);
//        }

//        [TestMethod]
//        public void Move_BouncesFromBottomWall()
//        {
//            // Arrange
//            var ball = new Ball(100, 480) { VelocityY = 5 }; // 480 + 20 + 5 > 500

//            // Act
//            ball.Move();

//            // Assert
//            Assert.AreEqual(475, ball.PositionY); // 480 + (-5)
//            Assert.AreEqual(-5, ball.VelocityY);
//        }

//        [TestMethod]
//        public void Move_NoBounceWhenWithinBounds()
//        {
//            // Arrange
//            var ball = new Ball(200, 200) { VelocityX = 3, VelocityY = -2 };

//            // Act
//            ball.Move();

//            // Assert
//            Assert.AreEqual(203, ball.PositionX);
//            Assert.AreEqual(198, ball.PositionY);
//            Assert.AreEqual(3, ball.VelocityX);
//            Assert.AreEqual(-2, ball.VelocityY);
//        }

//        [TestMethod]
//        public void PropertyChanged_RaisedForPositionChanges()
//        {
//            // Act
//            _ball.PositionX = 150;
//            var xChanged = _propertyChangedFired;
//            _propertyChangedFired = false;

//            _ball.PositionY = 200;
//            var yChanged = _propertyChangedFired;

//            // Assert
//            Assert.IsTrue(xChanged, "PropertyChanged powinien być wywołany dla PositionX");
//            Assert.IsTrue(yChanged, "PropertyChanged powinien być wywołany dla PositionY");
//        }

//        [TestMethod]
//        public void PropertyChanged_NotRaisedForSameValue()
//        {
//            // Arrange
//            _propertyChangedFired = false;
//            var originalX = _ball.PositionX;
//            var originalY = _ball.PositionY;

//            // Act
//            _ball.PositionX = originalX;
//            _ball.PositionY = originalY;

//            // Assert
//            Assert.IsFalse(_propertyChangedFired);
//        }

//        [TestMethod]
//        public void VelocityProperties_CanBeModified()
//        {
//            // Act
//            _ball.VelocityX = -5;
//            _ball.VelocityY = 10;

//            // Assert
//            Assert.AreEqual(-5, _ball.VelocityX);
//            Assert.AreEqual(10, _ball.VelocityY);
//        }

//        [TestMethod]
//        public void Diameter_ReturnsConstantValue()
//        {
//            Assert.AreEqual(20, Ball.Diameter);
//        }
//    }
//}