using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.ModelTest
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void MoveTest()
        {
            var ball = new Ball(100, 100, 700, 500);

            ball.Move();

            Assert.AreEqual(101, ball.PositionX);
            Assert.AreEqual(101, ball.PositionY);
        }

        [TestMethod]
        public void MoveBounceFromRightWallTest()
        {
            var ball = new Ball(680, 100, 700, 500) { Velocity = new VectorTo(5, 0) };

            ball.Move();

            Assert.AreEqual(-5, ball.Velocity.X);
            Assert.AreEqual(680, ball.PositionX);
        }

        [TestMethod]
        public void MoveBounceFromLeftWallTest()
        {
            var ball = new Ball(0, 100, 700, 500) { Velocity = new VectorTo(-5, 0) };

            ball.Move();

            Assert.AreEqual(5, ball.Velocity.X);
            Assert.AreEqual(0, ball.PositionX);
        }

        [TestMethod]
        public void MoveBounceFromBottomWallTest()
        {
            var ball = new Ball(100, 480, 700, 500) { Velocity = new VectorTo(0, 5) };

            ball.Move();

            Assert.AreEqual(-5, ball.Velocity.Y);
            Assert.AreEqual(480, ball.PositionY);
        }

        [TestMethod]
        public void MoveBounceFromTopWallTest()
        {
            var ball = new Ball(100, 0, 700, 500) { Velocity = new VectorTo(0, -5) };

            ball.Move();

            Assert.AreEqual(5, ball.Velocity.Y);
            Assert.AreEqual(0, ball.PositionY);
        }

        [TestMethod]
        public void MoveMultipleBouncesTest()
        {
            var ball = new Ball(680, 480, 700, 500) { Velocity = new VectorTo(5, 5) };

            ball.Move();

            Assert.AreEqual(-5, ball.Velocity.X);
            Assert.AreEqual(-5, ball.Velocity.Y);
            Assert.AreEqual(680, ball.PositionX);
            Assert.AreEqual(480, ball.PositionY);

            ball.Move();

            Assert.AreEqual(675, ball.PositionX);
            Assert.AreEqual(475, ball.PositionY);
        }
    }
}