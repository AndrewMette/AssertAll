using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class AreEqualShould
    {
        [TestMethod]
        public void FailWhenNotEqual()
        {
            AssertAll.AreEqual(1, 2, "1 and 2 are not equal, ya dummy");

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenLogicallyEqualButDifferentTypes()
        {
            int expected = 1;
            long actual = 1;
            AssertAll.AreEqual(expected, actual, "the types are different");

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void PassWhenEqual()
        {
            AssertAll.AreEqual(1, 1, "your logic is bad and you should feel bad");

            AssertAll.Execute();
        }
    }
}
