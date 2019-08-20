using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class FailShould : TestBase
    {
        [TestMethod]
        public void Fail()
        {
            AssertAll.Fail();

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
