using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class AreNotEqualShould : TestBase
    {
        [TestMethod]
        public void PassWhenNotEqual()
        {
            AssertAll.AreNotEqual(1, 2, "1 and 2 are not equal, ya dummy");

            AssertAll.Execute();
        }

        [TestMethod]
        public void PassWhenLogicallyEqualButDifferentTypes()
        {
            int expected = 1;
            long actual = 1;
            AssertAll.AreNotEqual(expected, actual, "the types are different");

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenEqual()
        {
            AssertAll.AreNotEqual(1, 1, "1 and 1 are indeed the same");

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
