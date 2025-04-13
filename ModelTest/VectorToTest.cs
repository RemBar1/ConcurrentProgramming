using ConcurrentProgramming.Model;

namespace ConcurrentProgramming.ModelTest
{
    [TestClass]
    public class VectorToTests
    {
        [TestMethod]
        public void VectorLengthTest()
        {
            var vector = new VectorTo(3, 4);

            Assert.AreEqual(5, vector.Length);
        }
    }
}