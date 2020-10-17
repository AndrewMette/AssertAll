using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
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
            string message = "a string is not an int";
            AssertAll.IsInstanceOfType("1", typeof(int), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.IsInstanceOfType", "Failure message assertion name was not altered");
            StringAssert.Contains(ex.Message, message, "Failure message missing");
            StringAssert.EndsWith(ex.Message, ">.", "Failure message followed by unexpected text");
        }
    }
}
