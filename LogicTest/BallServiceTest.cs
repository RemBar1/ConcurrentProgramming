using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConcurrentProgramming.Logic.Service;
using ConcurrentProgramming.Data;
using ConcurrentProgramming.Model;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace LogicTest
{
    [TestClass]
    public class BallServiceTest
    {
        private BallService ballService;
        private IBallRepository ballRepository;
        private const int BoardWidth = 800;
        private const int BoardHeight = 600;
        private const int BoardThickness = 10;

        [TestInitialize]
        public void Setup()
        {
            ballRepository = new BallRepository();
            ballService = new BallService(ballRepository, BoardWidth, BoardHeight, BoardThickness);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ballService.Dispose();
        }

        [TestMethod]
        public void CreateBalls_ValidCount_CreatesCorrectNumberOfBalls()
        {
            // Arrange
            int ballCount = 5;
            int diameter = 20;

            // Act
            ballService.CreateBalls(ballCount, diameter);

            // Assert
            Assert.AreEqual(ballCount, ballRepository.GetAll().Count());
            foreach (var ball in ballRepository.GetAll())
            {
                Assert.AreEqual(diameter, ball.Diameter);
                Assert.IsTrue(ball.Position.X >= diameter && ball.Position.X <= BoardWidth - BoardThickness * 2 - diameter);
                Assert.IsTrue(ball.Position.Y >= diameter && ball.Position.Y <= BoardHeight - BoardThickness * 2 - diameter);
            }
        }

        [TestMethod]
        public void CreateBalls_ValidCount_AssignsUniqueIds()
        {
            // Arrange
            int ballCount = 5;
            int diameter = 20;

            // Act
            ballService.CreateBalls(ballCount, diameter);

            // Assert
            var balls = ballRepository.GetAll().ToList();
            var uniqueIds = balls.Select(b => b.Id).Distinct();
            Assert.AreEqual(ballCount, uniqueIds.Count());
        }

        [TestMethod]
        public void CreateBalls_ValidCount_AssignsRandomColors()
        {
            // Arrange
            int ballCount = 10;
            int diameter = 20;

            // Act
            ballService.CreateBalls(ballCount, diameter);

            // Assert
            var balls = ballRepository.GetAll().ToList();
            var uniqueColors = balls.Select(b => b.Color).Distinct();
            Assert.IsTrue(uniqueColors.Count() > 1); // Should have multiple different colors
        }

        [TestMethod]
        public void StartSimulation_StartsAndStops_Successfully()
        {
            // Arrange
            ballService.CreateBalls(5, 20);
            var initialPositions = ballRepository.GetAll().Select(b => b.Position).ToList();

            // Act
            ballService.StartSimulation();
            Thread.Sleep(100); // Let the simulation run for a bit
            ballService.StopSimulation();

            // Assert
            var finalPositions = ballRepository.GetAll().Select(b => b.Position).ToList();
            Assert.AreNotEqual(initialPositions.Count, finalPositions.Count); // Positions should have changed
        }

        [TestMethod]
        public void StopSimulation_ClearsAllBalls()
        {
            // Arrange
            ballService.CreateBalls(5, 20);

            // Act
            ballService.StartSimulation();
            Thread.Sleep(50);
            ballService.StopSimulation();

            // Assert
            Assert.IsFalse(ballRepository.GetAll().Any());
        }

        [TestMethod]
        public void Dispose_DisposesServiceProperly()
        {
            // Arrange
            ballService.CreateBalls(5, 20);
            ballService.StartSimulation();

            // Act
            ballService.Dispose();

            // Assert
            Assert.IsFalse(ballRepository.GetAll().Any());
        }

        [TestMethod]
        public void CreateBalls_NoOverlap_BallsNotTooClose()
        {
            // Arrange
            int ballCount = 5;
            int diameter = 20;

            // Act
            ballService.CreateBalls(ballCount, diameter);

            // Assert
            var balls = ballRepository.GetAll().ToList();
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    var distance = (balls[i].Position - balls[j].Position).Length;
                    Assert.IsTrue(distance >= diameter, 
                        $"Balls {i} and {j} are too close. Distance: {distance}, Required: {diameter}");
                }
            }
        }
    }
} 