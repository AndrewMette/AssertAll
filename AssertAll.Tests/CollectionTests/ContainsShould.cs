using System.Collections.Generic;
using AssertAll.Exceptions;
using AssertAll.Tests.AssertAllTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.CollectionTests
{
    [TestClass]
    public class ContainsShould : TestBase
    {
        [TestMethod]
        public void FailWhenDoesNotContain()
        {
            var list1 = new List<string> {"1", "2"};
            AssertAll.Collections.Contains(list1, "3");

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void PassWhenDoesContain()
        {
            var list1 = new List<string> { "1", "2" };
            AssertAll.Collections.Contains(list1, "2");
            AssertAll.Execute();
        }
    }
}
