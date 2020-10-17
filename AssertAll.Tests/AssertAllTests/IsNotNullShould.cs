using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
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
            string message = "value is null";
            AssertAll.IsNotNull(null, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.IsNotNull", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }
    }
}
