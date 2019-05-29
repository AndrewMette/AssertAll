using System.Collections.Generic;
using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.CollectionTests
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
