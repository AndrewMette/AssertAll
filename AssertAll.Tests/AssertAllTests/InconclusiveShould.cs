using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class InconclusiveShould : TestBase
    {
        [TestMethod]
        public void ThrowAssertInconclusiveException()
        {
            AssertAll.Inconclusive("the test is inconclusive");

            Assert.ThrowsException<AssertAllInconclusiveException>(() => AssertAll.Execute());
        }
    }
}
