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
            var message = RandomValue.String();
            AssertAll.ExceptionMessageContainsAsync(async () => await ThrowExceptionWithMessage(message),
                doesNotContain);

            Assert.ThrowsException<AssertAllFailedException>(() => AssertAll.Execute());
        }

        [TestMethod]
        public void FailWhenExceptionIsNotThrown()
        {
            var doesNotContain = RandomValue.String();
            var message = RandomValue.String();
            AssertAll.ExceptionMessageContainsAsync(async () => await ThrowExceptionWithMessage(message, false),
                doesNotContain);

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
