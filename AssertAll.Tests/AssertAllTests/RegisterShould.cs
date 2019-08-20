using System;
using AssertAllNuget;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class RegisterShould
    {
        [TestMethod]
        public void ThrowExceptionIfNotUsingAssertAllAttribute()
        {
            var exception = Assert.ThrowsException<InvalidOperationException>(() => 
                AssertAll.IsTrue(true));

            Assert.AreEqual("AssertAll statements can only be used in a test with the AssertAllTestMethod attribute", exception.Message);
        }
    }
}
