using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests.AssertAllTests
{
    [TestClass]
    public class IsNotInstanceOfTypeShould : TestBase
    {
        [TestMethod]
        public void PassWhenTypesAreTheSame()
        {
            AssertAll.IsNotInstanceOfType("1", typeof(int));
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenTypesAreNotTheSame()
        {
            AssertAll.IsNotInstanceOfType(1, typeof(int));

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
