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
            var message = RandomValue.String();
            AssertAll.ExceptionMessageEquals(() => ThrowExceptionWithMessage(message),
                notEqual);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenExceptionIsNotThrown()
        {
            var notEqual = RandomValue.String();
            var message = RandomValue.String();
            AssertAll.ExceptionMessageEquals(() => ThrowExceptionWithMessage(message, false),
                notEqual);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
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
