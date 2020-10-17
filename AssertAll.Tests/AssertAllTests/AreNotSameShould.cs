using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class AreNotSameShould : TestBase
    {
        [TestMethod]
        public void PassWhenDifferentReferences()
        {
            var object1 = new object();
            var object2 = new object();
            AssertAll.AreNotSame(object1, object2, "these are the same");
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenSameReference()
        {
            var object1 = new object();
            var object2 = object1;
            string message = "these are the same";

            AssertAll.AreNotSame(object1, object2, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.AreNotSame", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }
    }
}
