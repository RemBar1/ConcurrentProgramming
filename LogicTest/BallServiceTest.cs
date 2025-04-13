using ConcurrentProgramming.Data;
using ConcurrentProgramming.Logic;
using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.LogicTest
{
    [TestClass]
    public class BallServiceTests
    {
        [TestMethod]
        public void CreateBallsTest()
        {
            var repository = new BallRepository();
            var service = new BallService(repository, 700, 500, 0);

            service.CreateBalls(5);

            Assert.AreEqual(5, repository.Balls.Count);
        }

        [TestMethod]
        public void CreateBallsCorrectPositionsTest()
        {
            var repository = new BallRepository();
            var service = new BallService(repository, 700, 500, 0);

            service.CreateBalls(10);

            for (int i = 0; i < repository.Balls.Count; i++)
            {
                for (int j = i + 1; j < repository.Balls.Count; j++)
                {
                    var ball1 = repository.Balls[i];
                    var ball2 = repository.Balls[j];
                    Assert.IsFalse(
                        Math.Abs(ball1.PositionX - ball2.PositionX) < Ball.Diameter &&
                        Math.Abs(ball1.PositionY - ball2.PositionY) < Ball.Diameter
                    );
                }
            }
        }

        [TestMethod]
        public void StartStopSimulationTest()
        {
            var repository = new BallRepository();
            var service = new BallService(repository, 700, 500, 0);
            var ball = new Ball(100, 100, 700, 500);
            repository.Add(ball);

            service.StartSimulation();
            Thread.Sleep(100);
            service.StopSimulation();

            Assert.AreNotEqual(100, ball.PositionX);
            Assert.AreNotEqual(100, ball.PositionY);
        }
    }
}