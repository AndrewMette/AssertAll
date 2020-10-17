using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class IsSubsetOfShould : TestBase
    {
        [TestMethod]
        public void FailWhenIsNotSubset()
        {
            var list1 = new List<string> {"1"};
            var list2 = new List<string> {"1", "2"};
            string message = "list items missing";
            AssertAll.Collections.IsSubsetOf(list2, list1, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.IsSubsetOf", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenIsSubset()
        {
            var list1 = new List<string> { "1" };
            var list2 = new List<string> { "1", "2" };
            AssertAll.Collections.IsSubsetOf(list1, list2);
            AssertAll.Execute();
        }
    }
}
