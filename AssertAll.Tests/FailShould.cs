using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class FailShould
    {
        [TestMethod]
        public void Fail()
        {
            AssertAll.Fail();

            Assert.ThrowsException<AssertFailedException>(() => AssertAll.Execute());
        }
    }
}
