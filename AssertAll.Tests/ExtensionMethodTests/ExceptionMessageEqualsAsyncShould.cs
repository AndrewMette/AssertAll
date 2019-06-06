using System;
using System.Threading.Tasks;
using AssertAll.Exceptions;
using AssertAll.Tests.AssertAllTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace AssertAll.Tests.ExtensionMethodTests
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
            var message = RandomValue.String();
            AssertAll.ExceptionMessageEqualsAsync(async () => await ThrowExceptionWithMessage(message),
                notEqual);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }
        
        [TestMethod]
        public void FailWhenExceptionIsNotThrown()
        {
            var notEqual = RandomValue.String();
            var message = RandomValue.String();
            AssertAll.ExceptionMessageEqualsAsync(() => ThrowExceptionWithMessage(message, false),
                notEqual);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
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
