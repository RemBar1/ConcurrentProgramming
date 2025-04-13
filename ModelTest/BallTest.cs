using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.ModelTest
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var ball = new Ball(10, 20);

            Assert.AreEqual(10, ball.PositionX);
            Assert.AreEqual(20, ball.PositionY);
            Assert.AreEqual(20, ball.Diameter);
            Assert.AreEqual(1, ball.Velocity.X);
            Assert.AreEqual(1, ball.Velocity.Y);
        }

        [TestMethod]
        public void PropertiesChangesTest()
        {
            var ball = new Ball(10, 20);

            ball.PositionX = 30;
            ball.PositionY = 40;
            ball.Diameter = 30;

            Assert.AreEqual(40, ball.PositionY);
            Assert.AreEqual(30, ball.PositionX);
            Assert.AreEqual(30, ball.Diameter);
        }

        [TestMethod]
        public void PropertyChangedTrueAndFalseTest()
        {
            var ball = new Ball(10, 20);
            var eventRaised = false;
            ball.PropertyChanged += (s, e) => eventRaised = true;

            ball.PositionX = 10;

            Assert.IsFalse(eventRaised);

            ball.PositionX = 20;

            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void BallVelocityTest()
        {
            var ball = new Ball(10, 20);
            var newVelocity = new VectorTo(5, 10);

            ball.Velocity = newVelocity;

            Assert.AreEqual(5, ball.Velocity.X);
            Assert.AreEqual(10, ball.Velocity.Y);
        }
    }
}