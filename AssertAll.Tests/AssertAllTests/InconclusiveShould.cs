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
            string message = "the test is inconclusive";
            AssertAll.Inconclusive(message);

            AssertAllInconclusiveException ex =
                    Assert.ThrowsException<AssertAllInconclusiveException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Inconclusive", "Warning message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Warning message missing or followed by unexpected text");
        }
    }
}
