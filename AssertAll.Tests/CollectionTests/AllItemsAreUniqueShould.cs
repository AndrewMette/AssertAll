using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
{
    [TestClass]
    public class AllItemsAreUniqueShould : TestBase
    {
        [TestMethod]
        public void FailWhenItemsNotUnique()
        {
            var list1 = new List<string> {"1", "1"};
            AssertAll.Collections.AllItemsAreUnique(list1);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void PassWhenItemsAreUnique()
        {
            var list1 = new List<string> { "1", "2" };
            AssertAll.Collections.AllItemsAreUnique(list1);
            AssertAll.Execute();
        }
    }
}
