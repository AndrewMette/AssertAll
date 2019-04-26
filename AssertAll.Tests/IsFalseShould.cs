using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class IsFalseShould
    {
        [TestMethod]
        public void PassWhenFalse()
        {
            AssertAll.IsFalse(1 == 2, "1 and 2 are not the same");

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenTrue()
        {
            AssertAll.IsFalse(1 == 1, "1 and 2 are the same");

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }
    }
}
