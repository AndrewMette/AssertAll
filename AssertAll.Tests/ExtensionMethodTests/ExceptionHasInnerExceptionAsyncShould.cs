using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.ExtensionMethodTests
{
    [TestClass]
    public class ExceptionHasInnerExceptionAsyncShould : TestBase
    {
        [TestMethod]
        public void PassWhenInnerExceptionIsOfCorrectType()
        {
            AssertAll.ThrowsExceptionWithInnerExceptionAsync<InvalidOperationException>(async () =>
                await ThrowExceptionWithInnerInvalidOperationException(true));

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenInnerExceptionIsNotCorrectType()
        {
            string message = "unexpected inner exception";
            AssertAll.ThrowsExceptionWithInnerExceptionAsync<ArgumentException>(async () =>
                await ThrowExceptionWithInnerInvalidOperationException(true), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(
                    ex.Message,
                    $"(1) AssertAll.ThrowsExceptionWithInnerExceptionAsync failed. Threw exception System.InvalidOperationException, but inner exception System.ArgumentException was expected. {message}\r\n",
                    "Unexpected failure message");
        }

        [TestMethod]
        public void FailWhenNoExceptionIsThrown()
        {
            string message = "missing exception";
            AssertAll.ThrowsExceptionWithInnerExceptionAsync<InvalidOperationException>(async () =>
                await ThrowExceptionWithInnerInvalidOperationException(false), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ThrowsExceptionWithInnerExceptionAsync failed. No exception thrown. InvalidOperationException inner exception was expected. {message}",
                    ex.Message,
                    "Unexpected failure message");
        }

        [TestMethod]
        public void FailWhenInnerExceptionIsNull()
        {
            string message = "missing inner exception";
            AssertAll.ThrowsExceptionWithInnerExceptionAsync<ArgumentException>(async () => 
                await ThrowNewException(), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ThrowsExceptionWithInnerExceptionAsync failed. Thrown exception has no inner exception. ArgumentException inner exception was expected. {message}",
                    ex.Message,
                    "Unexpected failure message");
        }


        private async Task ThrowExceptionWithInnerInvalidOperationException(bool shouldThrow)
        {
            if (shouldThrow)
            {
                throw new Exception(RandomValue.String(), new InvalidOperationException());
            }
        }

        private async Task ThrowNewException()
        {
            throw new Exception();
        }
    }
}
