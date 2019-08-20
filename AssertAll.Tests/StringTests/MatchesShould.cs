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
            AssertAll.Strings.Matches(someString, pattern);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}