using System;
using System.Collections.Generic;
using AssertAllNuget;
using AssertAllNuget.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAllTests.ExtensionMethodTests
{
    [TestClass]
    public class ExceptionHasInnerExceptionShould : TestBase
    {
        [TestMethod]
        public void PassWhenInnerExceptionIsOfCorrectType()
        {
            AssertAll.ThrowsExceptionWithInnerException<InvalidOperationException>(() =>
                ThrowExceptionWithInnerInvalidOperationException(true));

            AssertAll.Execute();
        }

        [TestMethod]
        public void FailWhenInnerExceptionIsNotCorrectType()
        {
            string message = "unexpected inner exception";
            AssertAll.ThrowsExceptionWithInnerException<ArgumentException>(() =>
                ThrowExceptionWithInnerInvalidOperationException(true), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            StringAssert.StartsWith(
                    ex.Message,
                    $"(1) AssertAll.ThrowsExceptionWithInnerException failed. Threw exception System.InvalidOperationException, but inner exception System.ArgumentException was expected. {message}\r\n",
                    "Unexpected failure message");
        }

        [TestMethod]
        public void FailWhenNoExceptionIsThrown()
        {
            string message = "missing exception";
            AssertAll.ThrowsExceptionWithInnerException<InvalidOperationException>(() =>
                ThrowExceptionWithInnerInvalidOperationException(false), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ThrowsExceptionWithInnerException failed. No exception thrown. InvalidOperationException inner exception was expected. {message}",
                    ex.Message,
                    "Unexpected failure message");
        }

        [TestMethod]
        public void FailWhenInnerExceptionIsNull()
        {
            string message = "missing inner exception";
            AssertAll.ThrowsExceptionWithInnerException<ArgumentException>(() => 
                throw new Exception(RandomValue.String()), message);

            AssertAllFailedException ex =
                    Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
            Assert.AreEqual(
                    $"(1) AssertAll.ThrowsExceptionWithInnerException failed. Thrown exception has no inner exception. ArgumentException inner exception was expected. {message}",
                    ex.Message,
                    "Unexpected failure message");
        }


        private void ThrowExceptionWithInnerInvalidOperationException(bool shouldThrow)
        {
            if (shouldThrow)
            {
                throw new Exception(RandomValue.String(), new InvalidOperationException());
            }
        }
    }
}
