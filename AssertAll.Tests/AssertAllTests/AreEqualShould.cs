using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class AreEqualShould : TestBase
    {
        [TestMethod]
        [DataRow(1, 2, "1 and 2 are not equal, ya dummy", DisplayName = "Fail when not equal")]
        [DataRow(1, 1L, "the types are different", DisplayName = "Fail when logically equal but different types")]
        public void FailWhenNotExactlyEqual(
                object expected, object actual, string message)
        {
            AssertAll.AreEqual(expected, actual, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.AreEqual", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenEqual()
        {
            AssertAll.AreEqual(1, 1, "your logic is bad and you should feel bad");

            AssertAll.Execute();
        }
    }
}
