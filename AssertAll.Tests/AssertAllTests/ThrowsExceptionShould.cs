using System;
using System.Collections.Generic;
using System.Linq;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAllTests.AssertAllTests
{
    [TestClass]
    public class ThrowsExceptionShould : TestBase
    {
        [TestMethod]
        public void PassWhenExceptionOfCorrectTypeIsThrown()
        {
            var emptyList = new List<object>();
            AssertAll.ThrowsException<InvalidOperationException>(() => emptyList.Single());

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenNoExceptionIsThrown()
        {
            var list = new List<object>{new object()};
            string message = "exception not thrown";
            AssertAll.ThrowsException<InvalidOperationException>(() => list.Single(), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.ThrowsException", "Failure message assertion name was not altered");
            StringAssert.EndsWith(ex.Message, message, "Failure message missing or followed by unexpected text");
        }

        [TestMethod]
        public void FailWhenWrongExceptionIsThrown()
        {
            var emptyList = new List<object>();
            string message = "incorrect exception thrown";
            AssertAll.ThrowsException<NullReferenceException>(() => emptyList.Single(), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(ex.Message, $"(1) AssertAll.ThrowsException", "Failure message assertion name was not altered");
            StringAssert.Contains(ex.Message, $"{message}{Environment.NewLine}Exception Message:", "Failure message missing or followed by unexpected text");
        }
    }
}
