using AssertAllNuget;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllSandbox
{
    [TestClass]
    public class UnitTest1
    {
        [AssertAllTestMethod]
        public void ShowAllTheAssertExceptions()
        {
            AssertAll.Fail("we'll see the rest in the test results");
            AssertAll.IsTrue(false);
            AssertAll.AreEqual(1,2);
        }
    }
}
