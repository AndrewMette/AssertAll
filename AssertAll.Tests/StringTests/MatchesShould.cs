using System.Text.RegularExpressions;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.StringTests
{
    [TestClass]
    public class MatchesShould : TestBase
    {
        [TestMethod]
        public void PassWhenMatches()
        {
            var someString = RandomValue.Int().ToString();
            var pattern = new Regex("[0-9]");
            AssertAll.Strings.Matches(someString, pattern);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenDoesNotMatch()
        {
            var someString = RandomValue.Int().ToString();
            var pattern = new Regex("[a-z]");
            string message = "strings don't match";
            AssertAll.Strings.Matches(someString, pattern, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Strings.Matches", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, $"{message}.", "Failure message missing or followed by unexpected text");
        }
    }
}