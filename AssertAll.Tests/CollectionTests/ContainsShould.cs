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
            AssertAll.Collections.Contains(list1, "3");

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
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
