using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class IsTrueShould : TestBase
    {
        [TestMethod]
        public void PassWhenTrue()
        {
            AssertAll.IsTrue(true, "oh dear, true is no longer true");

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenFalse()
        {
            string message = "1 and 2 are not the same ya dummy";
            AssertAll.IsTrue(1 == 2, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.IsTrue", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }
    }
}
