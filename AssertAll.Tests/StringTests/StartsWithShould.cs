using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.StringTests
{
    [TestClass]
    public class StartsWithShould : TestBase
    {
        [TestMethod]
        public void PassWhenStartsWIth()
        {
            var start = RandomValue.String();
            var end = RandomValue.String();

            AssertAll.Strings.StartsWith($"{start}{end}", start);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenDoesNotStartWith()
        {
            var value = RandomValue.String();
            var differentValue = RandomValue.String();
            string message = "string doesn't start with";

            AssertAll.Strings.StartsWith(value, differentValue, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Strings.StartsWith", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, $"{message}.", "Failure message missing or followed by unexpected text");
        }
    }
}