using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.StringTests
{
    [TestClass]
    public class ContainsShould : TestBase
    {
        [TestMethod]
        public void PassWhenContains()
        {
            var start = RandomValue.String();
            var middle = RandomValue.String();
            var end = RandomValue.String();
            AssertAll.Strings.Contains($"{start}{middle}{end}", middle);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenDoesNotContain()
        {
            string message = "substring not found";
            AssertAll.Strings.Contains(RandomValue.String(), RandomValue.String(), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Strings.Contains", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, $"{message}.", "Failure message missing or followed by unexpected text");
        }
    }
}
