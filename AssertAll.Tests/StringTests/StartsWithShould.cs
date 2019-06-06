using System;
using System.Collections.Generic;
using System.Text;
using AssertAll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAll.Tests.StringTests
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

            AssertAll.Strings.StartsWith(value, differentValue);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
    }
}