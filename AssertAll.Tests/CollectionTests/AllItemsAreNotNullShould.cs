using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class AllItemsAreNotNullShould : TestBase
    {
        [TestMethod]
        public void FailWhenItemIsNull()
        {
            var list1 = new List<string> {null};
            string message = "list contains null items";
            AssertAll.Collections.AllItemsAreNotNull(list1, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.AllItemsAreNotNull", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenNoItemIsNull()
        {
            var list1 = new List<string> { "1", "2" };
            AssertAll.Collections.AllItemsAreNotNull(list1);
            AssertAll.Execute();
        }
    }
}
