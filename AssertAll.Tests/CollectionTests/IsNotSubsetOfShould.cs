using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class IsNotSubsetOfShould : TestBase
    {
        [TestMethod]
        public void FailWhenIsSubset()
        {
            var list1 = new List<string> {"1"};
            var list2 = new List<string> {"1", "2"};
            string message = "list items unexpectedly found";
            AssertAll.Collections.IsNotSubsetOf(list1, list2, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.IsNotSubsetOf", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenIsNotSubset()
        {
            var list1 = new List<string> { "1", "2", "3" };
            var list2 = new List<string> { "1", "2" };
            AssertAll.Collections.IsNotSubsetOf(list1, list2);
            AssertAll.Execute();
        }
    }
}
