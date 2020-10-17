using System.Collections.Generic;
using System.Text.RegularExpressions;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class AreNotEqualShould : TestBase
    {
        [TestMethod]
        public void PassWhenNotEqual()
        {
            var list1 = new List<string> {"1"};
            var list2 = new List<string> {"1", "2"};
            AssertAll.Collections.AreNotEqual(list1, list2);
            AssertAll.Execute();
        }

        [TestMethod]
        public void PassWhenEquivalent()
        {
            var list1 = new List<string> { "2", "1" };
            var list2 = new List<string> { "1", "2" };
            AssertAll.Collections.AreNotEqual(list1, list2);
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenEqual()
        {
            var list1 = new List<string> { "1", "2" };
            var list2 = new List<string> { "1", "2" };
            string message = "list items are the same and in the same order";
            AssertAll.Collections.AreNotEqual(list1, list2, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.AreNotEqual", "Failure message assertion name was not altered");
            StringAssert.Matches(ex.Message, new Regex($".+{message}\\(.+\\.\\)$"), "Failure message missing or followed by unexpected text");
        }
    }
}
