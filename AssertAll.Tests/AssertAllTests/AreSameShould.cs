using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.AssertAllTests
{
    [TestClass]
    public class AreSameShould : TestBase
    {
        [TestMethod]
        public void PassWhenSameReferences()
        {
            var object1 = new object();
            var object2 = object1;
            AssertAll.AreSame(object1, object2, "these are not the same");
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenDifferentReferences()
        {
            var object1 = new object();
            var object2 = new object();

            AssertAll.AreSame(object1, object2, "these are not the same");

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
