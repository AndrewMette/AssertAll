using System.Collections.Generic;
using System.Text.RegularExpressions;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class AreEqualShould : TestBase
    {
        [TestMethod]
        public void FailWhenNotEqual()
        {
            var list1 = new List<string> {"1"};
            var list2 = new List<string> {"1", "2"};
            string message = "list items are different";
            AssertAll.Collections.AreEqual(list1, list2, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.AreEqual", "Failure message assertion name was not altered");
            StringAssert.Matches(ex.Message, new Regex($".+{message}\\(.+\\.\\)$"), "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void FailWhenEquivalent()
        {
            var list1 = new List<string> { "2", "1" };
            var list2 = new List<string> { "1", "2" };
            string message = "list items are different or in different order";
            AssertAll.Collections.AreEqual(list1, list2, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.AreEqual", "Failure message assertion name was not altered");
            StringAssert.Matches(ex.Message, new Regex($".+{message}\\(.+\\.\\)$"), "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenEqual()
        {
            var list1 = new List<string> { "1", "2" };
            var list2 = new List<string> { "1", "2" };
            AssertAll.Collections.AreEqual(list1, list2);
            AssertAll.Execute();
        }
    }
}
