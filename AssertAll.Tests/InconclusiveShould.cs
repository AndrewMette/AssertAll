using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class InconclusiveShould
    {
        [TestMethod]
        public void ThrowAssertInconclusiveException()
        {
            AssertAll.Inconclusive("the test is inconclusive");

            Assert.ThrowsException<AssertInconclusiveException>(() => AssertAll.Execute());
        }
    }
}
