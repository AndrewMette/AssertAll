using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class DoesNotContainShould : TestBase
    {
        [TestMethod]
        public void FailWhenDoesContain()
        {
            var list1 = new List<string> {"1", "2"};
            string message = "item unexpectedly found in list";
            AssertAll.Collections.DoesNotContain(list1, "2", message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Collections.DoesNotContain", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void PassWhenDoesNotContain()
        {
            var list1 = new List<string> { "1", "2" };
            AssertAll.Collections.DoesNotContain(list1, "3");
            AssertAll.Execute();
        }
    }
}
