using System.Collections.Generic;
using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.CollectionTests
{
    [TestClass]
    public class AreEqualShould : TestBase
    {
        [TestMethod]
        public void FailWhenNotEqual()
        {
            var list1 = new List<string> {"1"};
            var list2 = new List<string> {"1", "2"};
            AssertAll.Collections.AreEqual(list1, list2);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenEquivalent()
        {
            var list1 = new List<string> { "2", "1" };
            var list2 = new List<string> { "1", "2" };
            AssertAll.Collections.AreEqual(list1, list2);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void PassWhenEqual()
        {
            var list1 = new List<string> { "1", "2" };
            var list2 = new List<string> { "1", "2" };
            AssertAll.Collections.AreEqual(list1, list2);
            AssertAll.Execute();
        }
    }
}
