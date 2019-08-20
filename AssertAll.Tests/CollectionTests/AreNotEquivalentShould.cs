using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class AreNotEquivalentShould : TestBase
    {
        [TestMethod]
        public void FailWhenEquivalent()
        {
            var list1 = new List<string> { "2", "1" };
            var list2 = new List<string> { "1", "2" };
            AssertAll.Collections.AreNotEquivalent(list1, list2);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void PassWhenNotEquivalent()
        {
            var list1 = new List<string> { "1", "2" };
            var list2 = new List<string> { "1" };
            AssertAll.Collections.AreNotEquivalent(list1, list2);
            AssertAll.Execute();
        }
    }
}
