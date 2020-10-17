using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class AreNotEquivalentShould : TestBase
    {
        [TestMethod]
        public void FailWhenEquivalent()
        {
            var list1 = new List<string> { "2", "1" };
            var list2 = new List<string> { "1", "2" };
            string message = "list items are the same";
            AssertAll.Collections.AreNotEquivalent(list1, list2, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.AreNotEquivalent", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenNotEquivalent()
        {
            var list1 = new List<string> { "1", "2" };
            var list2 = new List<string> { "1" };
            AssertAll.Collections.AreNotEquivalent(list1, list2);
            AssertAll.Execute();
        }
    }
}
