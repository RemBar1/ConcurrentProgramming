﻿using ConcurrentProgramming.Data;
using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.DataTest
{
    [TestClass]
    public class BallRepositoryTests
    {
        [TestMethod]
        public void AddAddBallToCollectionTest()
        {
            var repository = new BallRepository();
            var ball = new Ball(0, 0, 700, 500);

            repository.Add(ball);

            Assert.AreEqual(1, repository.Balls.Count);
            Assert.AreSame(ball, repository.Balls.First());
        }

        [TestMethod]
        public void ClearTest()
        {
            var repository = new BallRepository();
            repository.Add(new Ball(0, 0, 700, 500));
            repository.Add(new Ball(10, 10, 700, 500));

            Assert.AreEqual(2, repository.Balls.Count);

            repository.Clear();

            Assert.AreEqual(0, repository.Balls.Count);
        }
    }
}