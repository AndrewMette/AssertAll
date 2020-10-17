using System;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.ExtensionMethodTests
{
    [TestClass]
    public class ExceptionMessageEqualsShould : TestBase
    {
        [TestMethod]
        public void PassWhenExceptionMethodEqualsArgument()
        {
            var message = RandomValue.String();
            AssertAll.ExceptionMessageEquals(() => ThrowExceptionWithMessage(message),
                message);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenExceptionMessageDoesNotEqualArgument()
        {
            var notEqual = RandomValue.String();
            var exceptionMessage = RandomValue.String();
            string assertFailureMessage = "unexpected exception message";
            AssertAll.ExceptionMessageEquals(() => ThrowExceptionWithMessage(exceptionMessage),
                notEqual, assertFailureMessage);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ExceptionMessageEquals failed. Expected message: {notEqual} Actual message: {exceptionMessage}. {assertFailureMessage}",
                    ex.Message,
                    "Unexpected failure message");
        }

        [TestMethod]
        public void FailWhenExceptionIsNotThrown()
        {
            var notEqual = RandomValue.String();
            var exceptionMessage = RandomValue.String();
            string assertFailureMessage = "missing exception";
            AssertAll.ExceptionMessageEquals(() => ThrowExceptionWithMessage(exceptionMessage, false),
                notEqual, assertFailureMessage);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ExceptionMessageEquals failed. No exception thrown. {assertFailureMessage}",
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
