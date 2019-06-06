using System;
using System.Collections.Generic;
using System.Text;
using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAll.Tests.StringTests
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

            AssertAll.Strings.EndsWith(value, substring);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}
