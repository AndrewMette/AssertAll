using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.AssertAllTests
{
    [TestClass]
    public class IsNotNullShould : TestBase
    {
        [TestMethod]
        public void PassWhenNull()
        {
            AssertAll.IsNotNull(1);
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenNotNull()
        {
            AssertAll.IsNotNull(null);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
