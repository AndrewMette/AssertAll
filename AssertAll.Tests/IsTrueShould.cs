using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class IsTrueShould : TestBase
    {
        [TestMethod]
        public void PassWhenTrue()
        {
            AssertAll.IsTrue(1 == 1, "1 and 1 are indeed the same");

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenFalse()
        {
            AssertAll.IsTrue(1 == 2, "1 and 2 are not the same ya dummy");

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
