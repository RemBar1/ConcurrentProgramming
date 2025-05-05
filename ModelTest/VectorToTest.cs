//using ConcurrentProgramming.Model;

//namespace ConcurrentProgramming.ModelTest
//{
//    [TestClass]
//    public class VectorToTest
//    {
//        [TestMethod]
//        public void ConstructorTest()
//        {
//            var vector = new Vector2(3, 4);

//            Assert.AreEqual(3, vector.X);
//            Assert.AreEqual(4, vector.Y);
//        }

//        [TestMethod]
//        public void LengthTest()
//        {
//            var vector = new Vector2(3, 4);

//            Assert.AreEqual(5, vector.Length);
//        }

//        [TestMethod]
//        public void PropertiesTest()
//        {
//            var vector = new Vector2(1, 2);

//            vector.X = 5;
//            vector.Y = 10;

//            Assert.AreEqual(5, vector.X);
//            Assert.AreEqual(10, vector.Y);
//        }

//        [TestMethod]
//        public void DefaultConstructorTest()
//        {
//            var vector = new Vector2();

//            Assert.AreEqual(0, vector.X);
//            Assert.AreEqual(0, vector.Y);
//        }
//    }
//}