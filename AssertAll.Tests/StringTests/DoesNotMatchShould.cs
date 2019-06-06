using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAll.Tests.StringTests
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
            var pattern = new Regex("[0-1]");
            AssertAll.Strings.DoesNotMatch(someString, pattern);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}