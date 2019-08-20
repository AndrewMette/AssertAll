using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class AreNotSameShould : TestBase
    {
        [TestMethod]
        public void PassWhenDifferentReferences()
        {
            var object1 = new object();
            var object2 = new object();
            AssertAll.AreNotSame(object1, object2, "these are the same");
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenSameReference()
        {
            var object1 = new object();
            var object2 = object1;

            AssertAll.AreNotSame(object1, object2, "these are the same");

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
