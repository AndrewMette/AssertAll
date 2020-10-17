using System;
using System.Threading.Tasks;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.ExtensionMethodTests
{
    [TestClass]
    public class ExceptionMessageContainsAsyncShould : TestBase
    {
        [TestMethod]
        public void PassWhenExceptionMethodContainsArgument()
        {
            var contains = RandomValue.String();
            var message = RandomValue.String() + contains + RandomValue.String();
            AssertAll.ExceptionMessageContainsAsync(async () => await ThrowExceptionWithMessage(message),
                contains);

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenExceptionMessageDoesNotContainArgument()
        {
            var doesNotContain = RandomValue.String();
            var exceptionMessage = RandomValue.String();
            string assertFailureMessage = "unexpected exception message";
            AssertAll.ExceptionMessageContainsAsync(async () => await ThrowExceptionWithMessage(exceptionMessage),
                doesNotContain, assertFailureMessage);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ExceptionMessageContainsAsync failed. {exceptionMessage} does not contain {doesNotContain}. {assertFailureMessage}",
                    ex.Message,
                    "Unexpected failure message");
        }

        [TestMethod]
        public void FailWhenExceptionIsNotThrown()
        {
            var doesNotContain = RandomValue.String();
            var exceptionMessage = RandomValue.String();
            string assertFailureMessage = "missing exception";
            AssertAll.ExceptionMessageContainsAsync(async () => await ThrowExceptionWithMessage(exceptionMessage, false),
                doesNotContain, assertFailureMessage);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ExceptionMessageContainsAsync failed. No exception thrown. {assertFailureMessage}",
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
