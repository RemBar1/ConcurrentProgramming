using ConcurrentProgramming.Model;
using System.Collections.ObjectModel;

namespace ConcurrentProgramming.Data.Tests
{
    [TestClass]
    public class BallRepositoryTest 
    {
        private IBallRepository _repository;
        private IBall _testBall;

        [TestInitialize]
        public void Setup()
        {
            _repository = new BallRepository();
            _testBall = new Ball(1, new Vector2(10, 10), 20);
        }

        [TestMethod]
        public void AddTest()
        {
            // Act
            _repository.Add(_testBall);

            // Assert
            Assert.AreEqual(1, _repository.Count);
            Assert.AreSame(_testBall, _repository.GetAll().First());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullTest()
        {
            // Act
            _repository.Add(null);
        }

        [TestMethod]
        public void ClearTest()
        {
            // Arrange
            _repository.Add(_testBall);
            _repository.Add(new Ball(2, new Vector2(20, 20), 20));

            // Act
            _repository.Clear();

            // Assert
            Assert.AreEqual(0, _repository.Count);
            Assert.AreEqual(0, _repository.GetAll().Count);
        }

        [TestMethod]
        public void GetAllTest()
        {
            // Arrange
            _repository.Add(_testBall);

            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(IReadOnlyList<IBall>));
            Assert.AreEqual(1, result.Count);
            Assert.AreSame(_testBall, result[0]);
        }

        [TestMethod]
        public void CountTest()
        {
            // Arrange
            _repository.Add(_testBall);
            _repository.Add(new Ball(2, new Vector2(20, 20), 20));

            // Act & Assert
            Assert.AreEqual(2, _repository.Count);

            // Act
            _repository.Clear();

            // Assert
            Assert.AreEqual(0, _repository.Count);
        }

        [TestMethod]
        public void BallCollectionTest()
        {
            // Act
            var ballsCollection = _repository.Balls;

            // Assert
            Assert.IsInstanceOfType(ballsCollection, typeof(ObservableCollection<IBall>));
        }

        [TestMethod]
        public void BallsPropertyTest()
        {
            // Act
            _repository.Add(_testBall);
            var ballsCollection = _repository.Balls;

            // Assert
            Assert.AreEqual(1, ballsCollection.Count);
            Assert.AreSame(_testBall, ballsCollection[0]);
        }
    }
}