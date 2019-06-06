using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.AssertAllTests
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
