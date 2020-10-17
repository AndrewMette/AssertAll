using System.Text.RegularExpressions;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.StringTests
{
    [TestClass]
    public class DoesNotMatchShould : TestBase
    {
        [TestMethod]
        public void PassWhenDoesNotMatch()
        {
            var someString = RandomValue.Int().ToString();
            var pattern = new Regex("[a-z]");
            AssertAll.Strings.DoesNotMatch(someString, pattern);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenMatches()
        {
            var someString = RandomValue.Int().ToString();
            var pattern = new Regex("[0-9]");
            string message = "strings match";
            AssertAll.Strings.DoesNotMatch(someString, pattern, message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.Strings.DoesNotMatch", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, $"{message}.", "Failure message missing or followed by unexpected text");
        }
    }
}