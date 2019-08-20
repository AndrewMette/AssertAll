using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class ThatShould : TestBase
    {
        [TestMethod]
        public void PassWhenExtensionMethodPasses()
        {
            AssertAll.That.TestMethod(true);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenExtensionMethodFails()
        {
            AssertAll.That.TestMethod(false);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }

    internal static class ExtensionMethods
    {
        internal static void TestMethod(this AssertAll source, bool passes)
        {
            if (passes == false)
            {
                AssertAll.Fail();
            }
        }
    }
}
