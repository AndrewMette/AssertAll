using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class IsFalseShould : TestBase
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
            string message = "true is false, Winston";
            AssertAll.IsFalse(true, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.IsFalse", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }
    }
}
