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
            string message = "forced failure";
            AssertAll.Fail(message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Fail", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }
    }
}
