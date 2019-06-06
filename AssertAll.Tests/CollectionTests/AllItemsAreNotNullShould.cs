using System.Collections.Generic;
using AssertAll.Exceptions;
using AssertAll.Tests.AssertAllTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.CollectionTests
{
    [TestClass]
    public class AllItemsAreNotNullShould : TestBase
    {
        [TestMethod]
        public void FailWhenItemIsNull()
        {
            var list1 = new List<string> {null};
            AssertAll.Collections.AllItemsAreNotNull(list1);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void PassWhenNoItemIsNull()
        {
            var list1 = new List<string> { "1", "2" };
            AssertAll.Collections.AllItemsAreNotNull(list1);
            AssertAll.Execute();
        }
    }
}
