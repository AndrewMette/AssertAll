using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class IsNullShould : TestBase
    {
        [TestMethod]
        public void PassWhenNull()
        {
            AssertAll.IsNull(null);
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenNotNull()
        {
            AssertAll.IsNull(1);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
