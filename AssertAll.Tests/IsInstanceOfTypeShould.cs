using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll.Tests
{
    [TestClass]
    public class IsInstanceOfTypeShould : TestBase
    {
        [TestMethod]
        public void PassWhenTypesAreTheSame()
        {
            AssertAll.IsInstanceOfType(1, typeof(int));
            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenTypesAreNotTheSame()
        {
            AssertAll.IsInstanceOfType("1", typeof(int));

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
