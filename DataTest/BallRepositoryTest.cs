//using ConcurrentProgramming.Data;
//using ConcurrentProgramming.Model;
//using System.Collections.ObjectModel;
//using System.ComponentModel;

//namespace ConcurrentProgramming.DataTest
//{
//    [TestClass]
//    public class BallRepositoryTest
//    {
//        private BallRepository _repository;
//        private TestBall _testBall;

//        // Testowa implementacja IBall
//        private class TestBall : IBall
//        {
//            public int PositionX { get; set; }
//            public int PositionY { get; set; }
//            public event PropertyChangedEventHandler? PropertyChanged;

//            public TestBall(int x, int y)
//            {
//                PositionX = x;
//                PositionY = y;
//            }

//            public void Move() { /* Nie potrzebujemy implementacji do testów */ }
//        }

//        [TestInitialize]
//        public void Setup()
//        {
//            _repository = new BallRepository();
//            _testBall = new TestBall(10, 20);
//        }

//        [TestMethod]
//        public void ConstructorTest()
//        {
//            Assert.AreEqual(0, _repository.Balls.Count);
//        }

//        [TestMethod]
//        public void AddBallWithCorrectPositionsTest()
//        {
//            _repository.AddBall(100, 150);

//            Assert.AreEqual(1, _repository.Balls.Count);
//            var ball = _repository.Balls[0];
//            Assert.AreEqual(100, ball.PositionX);
//            Assert.AreEqual(150, ball.PositionY);
//        }

//        [TestMethod]
//        public void ClearTest()
//        {
//            _repository.AddBall(10, 10);
//            _repository.AddBall(20, 20);

//            _repository.Clear();

//            Assert.AreEqual(0, _repository.Balls.Count);
//        }

//        [TestMethod]
//        public void BallsPropertyAtDifferentCollectionsTest()
//        {
//            var newCollection = new ObservableCollection<IBall>
//            {
//                _testBall,
//                new TestBall(30, 40)
//            };

//            _repository.Balls = newCollection;

//            Assert.AreEqual(2, _repository.Balls.Count);
//            Assert.AreSame(newCollection, _repository.Balls);
//        }

//        [TestMethod]
//        public void AddBallMultipleTimesTest()
//        {
//            _repository.AddBall(1, 1);
//            _repository.AddBall(2, 2);
//            _repository.AddBall(3, 3);

//            Assert.AreEqual(3, _repository.Balls.Count);
//        }

//        [TestMethod]
//        public void BallsPropertyRepositoryChangeTest()
//        {
//            var initialCount = _repository.Balls.Count;

//            _repository.AddBall(5, 5);

//            Assert.AreEqual(initialCount + 1, _repository.Balls.Count);
//        }
//    }
//}