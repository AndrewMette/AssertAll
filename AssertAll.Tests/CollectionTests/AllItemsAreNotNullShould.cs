using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
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
