using IvorChalton.GameOfLife.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IvorChalton.GameOfLife.Tests
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void Test_Point_HashCode()
        {
            Point p3 = new Point(11, 12);
            Point p4 = new Point(11, 12);
            Assert.AreEqual(p3, p4);

            Point p1 = new Point(1, 2);
            Point p2 = new Point(2, 1);
            Assert.AreNotEqual(p1, p2);
        }
    }
}
