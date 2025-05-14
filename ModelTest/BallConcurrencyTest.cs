using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.ModelTest
{
    [TestClass]
    public class BallConcurrencyTest
    {
        [TestMethod]
        public void ConcurrencyTest()
        {
            // Arrange
            var ball = new Ball(1, new Vector2(0, 0), 20);
            var exceptions = new System.Collections.Concurrent.ConcurrentQueue<Exception>();

            try
            {
                for (int j = 0; j < 1000; j++)
                {
                    var newPos = new Vector2(j, j);
                    ball.Position = newPos;
                    var pos = ball.Position;
                    Assert.AreEqual(newPos.X, pos.X);
                    Assert.AreEqual(newPos.Y, pos.Y);
                }
            }
            catch (Exception ex)
            {
                exceptions.Enqueue(ex);
            }
        }
    }
}