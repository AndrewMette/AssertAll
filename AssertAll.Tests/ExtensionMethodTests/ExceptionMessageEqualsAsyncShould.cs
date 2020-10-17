using System;
using System.Threading.Tasks;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.ExtensionMethodTests
{
    [TestClass]
    public class ExceptionMessageEqualsAsyncShould : TestBase
    {
        [TestMethod]
        public void PassWhenExceptionMethodEqualsArgument()
        {
            var message = RandomValue.String();
            AssertAll.ExceptionMessageEqualsAsync(async () => await ThrowExceptionWithMessage(message),
                message);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenExceptionMessageDoesNotEqualArgument()
        {
            var notEqual = RandomValue.String();
            var exceptionMessage = RandomValue.String();
            string assertFailureMessage = "unexpected exception message";
            AssertAll.ExceptionMessageEqualsAsync(async () => await ThrowExceptionWithMessage(exceptionMessage),
                notEqual, assertFailureMessage);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ExceptionMessageEqualsAsync failed. Expected message: {notEqual} Actual message: {exceptionMessage}. {assertFailureMessage}",
                    ex.Message,
                    "Unexpected failure message");
        }
        
        [TestMethod]
        public void FailWhenExceptionIsNotThrown()
        {
            var notEqual = RandomValue.String();
            var message = RandomValue.String();
            string assertFailureMessage = "missing exception";
            AssertAll.ExceptionMessageEqualsAsync(() => ThrowExceptionWithMessage(message, false),
                notEqual, assertFailureMessage);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ExceptionMessageEqualsAsync failed. No exception thrown. {assertFailureMessage}",
                    ex.Message,
                    "Unexpected failure message");
        }

        private async Task ThrowExceptionWithMessage(string message, bool shouldThrow = true)
        {
            if (shouldThrow)
            {
                throw new Exception(message);
            }
        }
    }
}
