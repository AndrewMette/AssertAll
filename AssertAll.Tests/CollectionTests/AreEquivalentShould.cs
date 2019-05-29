using System.Collections.Generic;
using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.CollectionTests
{
    [TestClass]
    public class AreEquivalentShould : TestBase
    {
        [TestMethod]
        public void FailWhenNotEquivalent()
        {
            var list1 = new List<string> {"1"};
            var list2 = new List<string> {"1", "2"};
            AssertAll.Collections.AreEquivalent(list1, list2);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void PassWhenEquivalent()
        {
            var list1 = new List<string> { "2", "1" };
            var list2 = new List<string> { "1", "2" };
            AssertAll.Collections.AreEquivalent(list1, list2);
            AssertAll.Execute();
        }

        [TestMethod]
        public void PassWhenEqual()
        {
            var list1 = new List<string> { "1", "2" };
            var list2 = new List<string> { "1", "2" };
            AssertAll.Collections.AreEquivalent(list1, list2);
            AssertAll.Execute();
        }
    }
}
