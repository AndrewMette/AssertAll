using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class AreNotEqualShould : TestBase
    {
        [TestMethod]
        public void PassWhenNotEqual()
        {
            AssertAll.AreNotEqual(1, 2, "1 and 2 are not equal, ya dummy");

            AssertAll.Execute();
        }

        [TestMethod]
        public void PassWhenLogicallyEqualButDifferentTypes()
        {
            int expected = 1;
            long actual = 1;
            AssertAll.AreNotEqual(expected, actual, "the types are different");

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenEqual()
        {
            string message = "1 and 1 are indeed the same";
            AssertAll.AreNotEqual(1, 1, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.AreNotEqual", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }
    }
}
