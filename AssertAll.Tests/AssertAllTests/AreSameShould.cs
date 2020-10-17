using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class AreSameShould : TestBase
    {
        [TestMethod]
        public void PassWhenSameReferences()
        {
            var object1 = new object();
            var object2 = object1;
            AssertAll.AreSame(object1, object2, "these are not the same");
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenDifferentReferences()
        {
            var object1 = new object();
            var object2 = new object();
            string message = "these are not the same";

            AssertAll.AreSame(object1, object2, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.AreSame", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }
    }
}
