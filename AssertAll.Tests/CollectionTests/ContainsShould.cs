using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class ContainsShould : TestBase
    {
        [TestMethod]
        public void FailWhenDoesNotContain()
        {
            var list1 = new List<string> {"1", "2"};
            string message = "item not found in list";
            AssertAll.Collections.Contains(list1, "3", message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.Contains", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenDoesContain()
        {
            var list1 = new List<string> { "1", "2" };
            AssertAll.Collections.Contains(list1, "2");
            AssertAll.Execute();
        }
    }
}
