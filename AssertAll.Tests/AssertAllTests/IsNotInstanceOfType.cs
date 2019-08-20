using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
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
