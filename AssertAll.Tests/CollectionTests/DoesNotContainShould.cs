using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.CollectionTests
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
