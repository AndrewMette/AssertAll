using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.AssertAllTests
{
    [TestClass]
    public class AreEqualShould : TestBase
    {
        [TestMethod]
        public void FailWhenNotEqual()
        {
            AssertAll.AreEqual(1, 2, "1 and 2 are not equal, ya dummy");
            
            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenLogicallyEqualButDifferentTypes()
        {
            int expected = 1;
            long actual = 1;
            AssertAll.AreEqual(expected, actual, "the types are different");

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void PassWhenEqual()
        {
            AssertAll.AreEqual(1, 1, "your logic is bad and you should feel bad");

            AssertAll.Execute();
        }
    }
}
