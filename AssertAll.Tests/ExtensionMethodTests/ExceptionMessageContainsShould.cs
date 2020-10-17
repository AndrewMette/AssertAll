using System;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.ExtensionMethodTests
{
    [TestClass]
    public class ExceptionMessageContainsShould : TestBase
    {
        [TestMethod]
        public void PassWhenExceptionMethodContainsArgument()
        {
            var contains = RandomValue.String();
            var message = RandomValue.String() + contains + RandomValue.String();
            AssertAll.ExceptionMessageContains(() => ThrowExceptionWithMessage(message),
                contains);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenExceptionMessageDoesNotContainArgument()
        {
            var doesNotContain = RandomValue.String();
            var exceptionMessage = RandomValue.String();
            string assertFailureMessage = "unexpected exception message";
            AssertAll.ExceptionMessageContains(() => ThrowExceptionWithMessage(exceptionMessage),
                doesNotContain, assertFailureMessage);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ExceptionMessageContains failed. {exceptionMessage} does not contain {doesNotContain}. {assertFailureMessage}",
                    ex.Message,
                    "Unexpected failure message");
        }

        [TestMethod]
        public void FailWhenExceptionIsNotThrown()
        {
            var notEqual = RandomValue.String();
            var exceptionMessage = RandomValue.String();
            string assertFailureMessage = "missing exception";
            AssertAll.ExceptionMessageContains(() => ThrowExceptionWithMessage(exceptionMessage, false),
                notEqual, assertFailureMessage);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ExceptionMessageContains failed. No exception thrown. {assertFailureMessage}",
                    ex.Message,
                    "Unexpected failure message");
        }

        private void ThrowExceptionWithMessage(string message, bool shouldThrow = true)
        {
            if (shouldThrow)
            {
                throw new Exception(message);
            }
        }
    }
}
