using AssertAllNuget;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests
{
    public class TestBase
    {
        [TestInitialize]
        public void Initialize()
        {
            AssertAll.ReadyForUsage = true;
        }

        [TestCleanup]
        public void Cleanup()
        {
            AssertAll.ReadyForUsage = false;
        }
    }
}