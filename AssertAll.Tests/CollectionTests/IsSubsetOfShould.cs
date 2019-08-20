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
            AssertAll.Collections.IsSubsetOf(list2, list1);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
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
