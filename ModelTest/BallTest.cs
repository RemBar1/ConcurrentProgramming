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

            Assert.AreEqual(101, ball.PositionX); // default velocity.X = 1
            Assert.AreEqual(101, ball.PositionY); // default velocity.Y = 1
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
    }
}