using System;
using AssertAll.Exceptions;
using AssertAll.Tests.AssertAllTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAll.Tests.ExtensionMethodTests
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
            var message = RandomValue.String();
            AssertAll.ExceptionMessageContains(() => ThrowExceptionWithMessage(message),
                doesNotContain);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenExceptionIsNotThrown()
        {
            var notEqual = RandomValue.String();
            var message = RandomValue.String();
            AssertAll.ExceptionMessageContains(() => ThrowExceptionWithMessage(message, false),
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
