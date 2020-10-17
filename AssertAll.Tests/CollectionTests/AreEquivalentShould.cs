using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class AreEquivalentShould : TestBase
    {
        [TestMethod]
        public void FailWhenNotEquivalent()
        {
            var list1 = new List<string> {"1"};
            var list2 = new List<string> {"1", "2"};
            string message = "list items are different";
            AssertAll.Collections.AreEquivalent(list1, list2, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.AreEquivalent", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenEquivalent()
        {
            var list1 = new List<string> { "2", "1" };
            var list2 = new List<string> { "1", "2" };
            AssertAll.Collections.AreEquivalent(list1, list2);
            AssertAll.Execute();
        }

        [TestMethod]
        public void PassWhenEqual()
        {
            var list1 = new List<string> { "1", "2" };
            var list2 = new List<string> { "1", "2" };
            AssertAll.Collections.AreEquivalent(list1, list2);
            AssertAll.Execute();
        }
    }
}
