using System.Collections.Generic;
using AssertAll.Exceptions;
using AssertAll.Tests.AssertAllTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.CollectionTests
{
    [TestClass]
    public class DoesNotContainShould : TestBase
    {
        [TestMethod]
        public void FailWhenDoesContain()
        {
            var list1 = new List<string> {"1", "2"};
            AssertAll.Collections.DoesNotContain(list1, "2");

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void PassWhenDoesNotContain()
        {
            var list1 = new List<string> { "1", "2" };
            AssertAll.Collections.DoesNotContain(list1, "3");
            AssertAll.Execute();
        }
    }
}
