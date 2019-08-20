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
            AssertAll.Collections.IsNotSubsetOf(list1, list2);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
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
