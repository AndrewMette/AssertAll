using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.AssertAllTests
{
    [TestClass]
    public class IsFalseShould : TestBase
    {
        [TestMethod]
        public void PassWhenFalse()
        {
            AssertAll.IsFalse(1 == 2, "1 and 2 are not the same");

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenTrue()
        {
            AssertAll.IsFalse(true, "1 and 2 are the same");

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
