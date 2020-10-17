using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class AllItemsAreUniqueShould : TestBase
    {
        [TestMethod]
        public void FailWhenItemsNotUnique()
        {
            var list1 = new List<string> {"1", "1"};
            string message = "list contains duplicate items";
            AssertAll.Collections.AllItemsAreUnique(list1, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.AllItemsAreUnique", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenItemsAreUnique()
        {
            var list1 = new List<string> { "1", "2" };
            AssertAll.Collections.AllItemsAreUnique(list1);
            AssertAll.Execute();
        }
    }
}
