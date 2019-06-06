using System.Collections.Generic;
using AssertAll.Exceptions;
using AssertAll.Tests.AssertAllTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.CollectionTests
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
