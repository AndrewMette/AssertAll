using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.StringTests
{
    [TestClass]
    public class EndsWithShould : TestBase
    {
        [TestMethod]
        public void PassWhenValueEndsWithSubstring()
        {
            var start = RandomValue.String();
            var end = RandomValue.String();

            AssertAll.Strings.EndsWith($"{start}{end}", end);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenValueDoesNotEndsWithSubstring()
        {
            var value = RandomValue.String();
            var substring = RandomValue.String();
            string message = "string doesn't end with";

            AssertAll.Strings.EndsWith(value, substring, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Strings.EndsWith", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, $"{message}.", "Failure message missing or followed by unexpected text");
        }
    }
}
