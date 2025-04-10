//using ConcurrentProgramming.Data;
//using ConcurrentProgramming.Logic;
//using ConcurrentProgramming.Model;
//using System.Collections.ObjectModel;
//using System.ComponentModel;

//namespace ConcurrentProgramming.LogicTest
//{
//    [TestClass]
//    public class BallPhysicsTest
//    {
//        private TestBallRepository _testRepository;
//        private BallService _physics;

//        // Testowa implementacja IBallRepository
//        private class TestBallRepository : IBallRepository
//        {
//            public ObservableCollection<IBall> Balls { get; } = new();
//            public int AddBallCallCount { get; private set; }
//            public int ClearCallCount { get; private set; }

//            public void AddBall(int x, int y)
//            {
//                Balls.Add(new TestBall(x, y));
//                AddBallCallCount++;
//            }

//            public void Clear()
//            {
//                Balls.Clear();
//                ClearCallCount++;
//            }
//        }

//        // Testowa implementacja IBall
//        private class TestBall : IBall
//        {
//            public int PositionX { get; set; }
//            public int PositionY { get; set; }
//            public int MoveCallCount { get; private set; }
//            public event PropertyChangedEventHandler? PropertyChanged;

//            public TestBall(int x, int y)
//            {
//                PositionX = x;
//                PositionY = y;
//            }

//            public void Move() => MoveCallCount++;
//        }

//        [TestInitialize]
//        public void Setup()
//        {
//            _testRepository = new TestBallRepository();
//            _physics = new BallService(_testRepository);
//        }

//        [TestMethod]
//        public void CreateBallsTest()
//        {
//            _physics.CreateBalls();

//            Assert.IsTrue(_testRepository.Balls.Count >= 5 && _testRepository.Balls.Count <= 10);
//        }

//        [TestMethod]
//        public void StartSimulationTest()
//        {
//            _testRepository.AddBall(100, 100);
//            _testRepository.AddBall(200, 200);
//            var ball1 = (TestBall)_testRepository.Balls[0];
//            var ball2 = (TestBall)_testRepository.Balls[1];

//            _physics.StartSimulation();
//            Thread.Sleep(100);
//            _physics.StopSimulation();

//            Assert.IsTrue(ball1.MoveCallCount > 0);
//            Assert.IsTrue(ball2.MoveCallCount > 0);
//        }

//        [TestMethod]
//        public void StopSimulationTest()
//        {
//            _testRepository.AddBall(100, 100);
//            var ball = (TestBall)_testRepository.Balls[0];
//            _physics.StartSimulation();

//            Thread.Sleep(50);
//            var callsBeforeStop = ball.MoveCallCount;
//            _physics.StopSimulation();
//            Thread.Sleep(50);
//            var callsAfterStop = ball.MoveCallCount;

//            Assert.IsTrue(callsBeforeStop > 0);
//            Assert.AreEqual(callsBeforeStop, callsAfterStop);
//        }

//        [TestMethod]
//        public void CreateMultipleBallsTest()
//        {
//            _physics.CreateBalls();

//            foreach (var ball in _testRepository.Balls)
//            {
//                Assert.IsTrue(ball.PositionX >= 0 && ball.PositionX <= 600);
//                Assert.IsTrue(ball.PositionY >= 0 && ball.PositionY <= 400);
//            }
//        }

//        [TestMethod]
//        public void SimulationWithoutBallsTest()
//        {
//            _physics.StartSimulation();
//            Thread.Sleep(100);
//            _physics.StopSimulation();

//            Assert.AreEqual(0, _testRepository.Balls.Count);
//        }
//    }
//}