//using ConcurrentProgramming.Data;
//using ConcurrentProgramming.Logic.Service;
//using ConcurrentProgramming.Model;

//namespace ConcurrentProgramming.LogicTest
//{
//    [TestClass]
//    public class BallServiceTest
//    {
//        private BallRepository repository;
//        private BallService service;

//        [TestInitialize]
//        public void Initialize()
//        {
//            repository = new BallRepository();
//            service = new BallService(repository, 700, 500, 0);
//        }

//        [TestMethod]
//        public void CreateBallsTest()
//        {
//            service.CreateBalls(5, 20);

//            Assert.AreEqual(5, repository.Balls.Count);
//        }

//        [TestMethod]
//        public void CreateBallsCorrectPositionsTest()
//        {
//            service.CreateBalls(10, 20);

//            for (int i = 0; i < repository.Balls.Count; i++)
//            {
//                for (int j = i + 1; j < repository.Balls.Count; j++)
//                {
//                    var ball1 = repository.Balls[i];
//                    var ball2 = repository.Balls[j];
//                    Assert.IsFalse(
//                        Math.Abs(ball1.PositionX - ball2.PositionX) < 20 &&
//                        Math.Abs(ball1.PositionY - ball2.PositionY) < 20
//                    );
//                }
//            }
//        }

//        [TestMethod]
//        public void StartStopSimulationTest()
//        {
//            var ball = new Ball(100, 100, 20);
//            repository.Add(ball);

//            service.StartSimulation();
//            Thread.Sleep(100);
//            service.StopSimulation();

//            Assert.AreNotEqual(100, ball.PositionX);
//            Assert.AreNotEqual(100, ball.PositionY);
//        }


//        [TestMethod]
//        public void MoveBallTest()
//        {
//            var ball = new Ball(100, 100, 20) { Velocity = new Vector2(5, 10) };
//            repository.Add(ball);

//            service.MoveBall(ball);

//            Assert.AreEqual(105, ball.PositionX);
//            Assert.AreEqual(110, ball.PositionY);
//        }
//        [TestMethod]
//        public void MoveBallShouldBounceFromLeftWallTest()
//        {
//            var ball = new Ball(0, 100, 20) { Velocity = new Vector2(-5, 2), Diameter = 20 };

//            service.MoveBall(ball);

//            Assert.AreEqual(5, ball.Velocity.X);
//            Assert.AreEqual(0, ball.PositionX);
//        }

//        [TestMethod]
//        public void MoveBallShouldBounceFromRightWallTest()
//        {
//            var ball = new Ball(680, 100, 20) { Velocity = new Vector2(5, 2), Diameter = 20 };

//            service.MoveBall(ball);

//            Assert.AreEqual(-5, ball.Velocity.X);
//            Assert.AreEqual(680, ball.PositionX);
//        }

//        [TestMethod]
//        public void MoveBallShouldBounceFromTopWallTest()
//        {
//            var ball = new Ball(100, 0, 20) { Velocity = new Vector2(2, -5), Diameter = 20 };

//            service.MoveBall(ball);

//            Assert.AreEqual(5, ball.Velocity.Y);
//            Assert.AreEqual(0, ball.PositionY);
//        }

//        [TestMethod]
//        public void MoveBallShouldBounceFromBottomWallTest()
//        {
//            var ball = new Ball(100, 480, 20) { Velocity = new Vector2(2, 5), Diameter = 20 };

//            service.MoveBall(ball);

//            Assert.AreEqual(-5, ball.Velocity.Y);
//            Assert.AreEqual(480, ball.PositionY);
//        }
//    }
//}